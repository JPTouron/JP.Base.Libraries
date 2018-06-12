using JP.Base.DAL.ADO.Commands;
using JP.Base.DAL.ADO.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;

namespace JP.Base.DAL.ADO.Implementations.Connections.Base
{
    internal enum CommandReturnType
    {
        Scalar = 0,
        Reader, // Tabular
        NonQuery
    }

    /// <summary>
    /// Define los comportamientos comunes a todas las conexiones a bases de datos que se utilizan en
    /// este ensamblado
    /// </summary>
    internal abstract class DbAdoConnection : IDbAdoConnection, IDisposable
    {
        protected DbCommand command = null;
        protected DbConnection conn = null;
        protected DbProviderFactory dbProviderFactory = null;
        protected DbTransaction transaction = null;
        private string connString = string.Empty;
        private string dataProvider;
        private bool isConnDisposed;

        public DbAdoConnection() : this(ConfigurationManager.AppSettings["DataProvider"], ConfigurationManager.AppSettings["ConnectionString"])
        {
        }

        public DbAdoConnection(string dataProvider, string connString)
        {
            this.connString = connString;
            this.dataProvider = dataProvider;
            Debug.WriteLine($"created AdoConnection:{dataProvider} - {this.connString}");
        }

        public string ConnHash
        {
            get
            {
                return conn == null ? "" : conn.GetHashCode().ToString();
            }
        }

        public bool IsDisposed { get; private set; }

        public void AddCommandParameter(ParameterData paramData)
        {
            if (paramData.Value == null)
            {
                AddParameter(paramData.Name, DBNull.Value, paramData.Direction, paramData.Type, paramData.Size);
            }
            else
            {
                AddParameter(paramData.Name, paramData.Value, paramData.Direction, paramData.Type, paramData.Size);

                var param = command.Parameters[command.Parameters.Count - 1];
                var valueAsDate = Convert.ToDateTime(paramData.Value, CultureInfo.InvariantCulture);

                if ((param.DbType == DbType.String || param.DbType == DbType.AnsiString) && string.IsNullOrEmpty(paramData.ToString()))
                    param.Value = DBNull.Value;
                else if ((param.DbType == DbType.Date || param.DbType == DbType.DateTime) && valueAsDate == DateTime.MinValue)
                    param.Value = DBNull.Value;
            }
        }

        public void AddCommandParameter(IEnumerable<ParameterData> Parametros)
        {
            if (Parametros != null)
                foreach (ParameterData Param in Parametros)
                    AddCommandParameter(Param);
        }

        public void BeginTransaction()
        {
            if (conn != null && transaction == null)
                transaction = conn.BeginTransaction();

            if (command != null)
                command.Transaction = transaction;
        }

        public void Close()
        {
            Debug.WriteLine($"OnClose(), conn ==null: { conn == null}");

            if (conn != null)
            {
                Debug.WriteLine($"conn id: { conn.GetHashCode()}");
                Debug.WriteLine($"conn state: { conn.State.ToString()}");

                if (conn.State == ConnectionState.Open)
                {
                    Debug.WriteLine($"transaction ==null: { transaction == null}");

                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                        Debug.WriteLine($"disposed and nulled tran");
                    }

                    conn.Close();
                    conn.Disposed -= OnConnDisposed;
                    //isConnDisposed = false;

                    Debug.WriteLine($"closed conn, and removed on disposed event ");
                }
            }

            Debug.WriteLine($"exited OnClose()");
        }

        public void CommitTransaction()
        {
            Debug.WriteLine($"OnCommitTransaction()");

            transaction.Commit();
        }

        public void CreateCommand(CommandData data)
        {
            if (command != null)
            {
                command.Dispose();
                command = null;
            }

            command = conn.CreateCommand();
            command.CommandType = data.CommandType;
            command.CommandText = data.CommandText;
            command.CommandTimeout = data.CommandTimeout;

            AddCommandParameter(data.Params);
        }

        public void CreateCommand(string commandText)
        {
            CreateCommand(new CommandData { CommandText = commandText });
        }

        public void Dispose()
        {
            Debug.WriteLine($"OnDipose()");

            if (!IsDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                IsDisposed = true;
            }

            Debug.WriteLine($"exited OnDipose()");
        }

        public int ExecuteNonQueryCommand()
        {
            SetTransactionToCommand();
            return ExecuteNonQuery();
        }

        public DataTable ExecuteReaderCommand()
        {
            SetTransactionToCommand();
            return ExecuteReader();
        }

        public T ExecuteScalarCommand<T>()
        {
            SetTransactionToCommand();
            return ExecuteScalar<T>();
        }

        public bool Open()
        {
            Debug.WriteLine($"OnOpen()");

            var result = false;

            try
            {
                Debug.WriteLine($"Opening connection, conn ==null: { conn == null}");

                if (conn == null || isConnDisposed)
                    CreateConnection();

                Debug.WriteLine($"conn state: { conn.State.ToString()}");

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                Debug.WriteLine($"conn state: { conn.State.ToString()}");
                Debug.WriteLine($"conn id: { ConnHash}");

                result = true;
            }
            catch
            {
                Debug.WriteLine($"error on dbadoconnection conn!=null: { conn != null}");

                if (conn != null)
                    conn.Dispose();

                conn = null;
                throw;
            }

            return result;
        }

        public void RollbackTansacton()
        {
            Debug.WriteLine($"OnRollbackTansacton()");

            if (transaction != null)
            {
                Debug.WriteLine($"Rolling back and destroying tran");

                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
            }
        }

        protected internal abstract void AddParameter(string Nombre, object Valor, ParameterDirection Direccion, DbType Tipo, int size);

        protected internal abstract int ExecuteNonQuery();

        protected internal abstract DataTable ExecuteReader();

        protected internal abstract T ExecuteScalar<T>();

        protected virtual void Dispose(bool disposing)
        {
            Debug.WriteLine($"OnDipose(bool)");

            if (disposing)
            {
                //managed
                dbProviderFactory = null;

                if (conn != null)
                {
                    Debug.WriteLine($"closing and disposing inernal conn");

                    conn.Close();
                    conn.Dispose();
                    conn.Disposed -= OnConnDisposed;
                }

                if (transaction != null)
                {
                    Debug.WriteLine($"disposing tran");

                    transaction.Dispose();
                }

                if (command != null)
                {
                    Debug.WriteLine($"disposing command");

                    command.Dispose();
                }
            }

            Debug.WriteLine($"exited OnDipose(bool)");

            //unmanaged
        }

        private void CreateConnection()
        {
            Debug.WriteLine($"OnCreateConnection()");

            SetDbProviderFactory();
            conn = dbProviderFactory.CreateConnection();
            conn.ConnectionString = connString;
            conn.Disposed += OnConnDisposed;
            isConnDisposed = false; // reset this var's value
            Debug.WriteLine($"Created Connection{ConnHash}");
        }

        private void OnConnDisposed(object sender, EventArgs e)
        {
            Debug.WriteLine($"OnConnDisposed()");

            isConnDisposed = true;
            Debug.WriteLine($"Disposed internal Connection {((DbConnection)sender).GetHashCode()}");
        }

        private void SetDbProviderFactory()
        {
            if (dbProviderFactory == null)
                dbProviderFactory = DbProviderFactories.GetFactory(dataProvider);
        }

        private void SetTransactionToCommand()
        {
            if (transaction != null)
                command.Transaction = transaction;
        }

        ~DbAdoConnection()
        {
            Debug.WriteLine($"DbAdoConnection destructor");

            Dispose(false);
        }
    }
}
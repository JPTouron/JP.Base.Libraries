using JP.Base.DAL.ADO.Commands;
using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Helpers;
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
        private string connstring = string.Empty;
        private bool isConnDisposed;

        public DbAdoConnection() : this(ConfigurationManager.AppSettings["DataProvider"], ConfigurationManager.AppSettings["ConnectionString"])
        {
        }

        public DbAdoConnection(string dataProvider, string connString)
        {
            connstring = connString;
            dbProviderFactory = DbProviderFactories.GetFactory(dataProvider);

            Debug.WriteLine($"created AdoConnection:{dataProvider} - {connstring}");
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
            if (paramData.ValorParametro == null)
            {
                AddParameter(paramData.NombreParametro, DBNull.Value, paramData.DireccionParametro, paramData.Tipo, paramData.Size);
            }
            else
            {
                AddParameter(paramData.NombreParametro, paramData.ValorParametro, paramData.DireccionParametro, paramData.Tipo, paramData.Size);

                var param = command.Parameters[command.Parameters.Count - 1];
                var valueAsDate = Convert.ToDateTime(paramData.ValorParametro, CultureInfo.InvariantCulture);

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
            return (int)ExecuteCommand(CommandReturnType.NonQuery);
        }

        public DataTable ExecuteReaderCommand()
        {
            return (DataTable)ExecuteCommand(CommandReturnType.Reader);
        }

        public object ExecuteScalarCommand()
        {
            return ExecuteCommand(CommandReturnType.Scalar);
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

        protected internal abstract object ExecuteScalar();

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

        protected DataTable ReaderToTable(DbDataReader reader)
        {
            var result = new DataTable();
            var adapter = new DataReaderAdapter();
            result.Locale = CultureInfo.CurrentCulture;

            adapter.Fill_From_Reader(result, reader);

            reader.Close();
            adapter.Dispose();

            return result;
        }

        private void CreateConnection()
        {
            Debug.WriteLine($"OnCreateConnection()");

            conn = dbProviderFactory.CreateConnection();
            conn.ConnectionString = connstring;
            conn.Disposed += OnConnDisposed;
            isConnDisposed = false; // reset this var's value
            Debug.WriteLine($"Created Connection{ConnHash}");
        }

        private object DoExecuteCommand(CommandReturnType returnType)
        {
            object retVal = null;
            switch (returnType)
            {
                case CommandReturnType.Scalar:
                    retVal = ExecuteScalar();
                    break;

                case CommandReturnType.Reader:
                    retVal = ExecuteReader();
                    break;

                case CommandReturnType.NonQuery:
                    retVal = ExecuteNonQuery();
                    break;
            }
            return retVal;
        }

        private object ExecuteCommand(CommandReturnType returnType)
        {
            if (transaction != null)
                command.Transaction = transaction;

            //closeConn = PrepareConnAndTranForExecution();
            var retVal = DoExecuteCommand(returnType);

            return retVal;
        }

        private void OnConnDisposed(object sender, EventArgs e)
        {
            Debug.WriteLine($"OnConnDisposed()");

            isConnDisposed = true;
            Debug.WriteLine($"Disposed internal Connection {((DbConnection)sender).GetHashCode()}");
        }

        ~DbAdoConnection()
        {
            Debug.WriteLine($"DbAdoConnection destructor");

            Dispose(false);
        }
    }
}
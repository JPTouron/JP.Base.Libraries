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
        private bool transactionWasCommited;

        /// <summary>
        /// Inicializa el objeto con los valores para el DataProvider y el ConnectionString desde el
        /// archivo Web.configo o App.config
        /// </summary>
        public DbAdoConnection() : this(ConfigurationManager.AppSettings["DataProvider"], ConfigurationManager.AppSettings["ConnectionString"])
        {

        }

        public DbAdoConnection(string dataProvider, string connString)
        {
            connstring = connString;
            dbProviderFactory = DbProviderFactories.GetFactory(dataProvider);
        }

        public string ConnHash
        {
            get
            {
                return conn == null ? "" : conn.GetHashCode().ToString();
            }
        }

        public bool IsDisposed { get; private set; }

        internal enum CommandReturnType
        {
            Scalar = 0,
            Reader, // Tabular
            NonQuery
        }

        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y
        /// establece los valores null del parametro si el parametro valor es un String.Empty o un
        /// DateTime.MinValue o null
        /// </summary>
        /// <param name="paramData">Nombre, valor y direccion del parametro</param>
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

        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y
        /// establece los valores null del parametro si el parametro valor es un String.Empty o un
        /// DateTime.MinValue o null
        /// </summary>
        /// <param name="Parametros">Lista de datos del parametro como nombre, valor y direccion</param>
        public void AddCommandParameter(IEnumerable<ParameterData> Parametros)
        {
            if (Parametros != null)
                foreach (ParameterData Param in Parametros)
                    AddCommandParameter(Param);
        }

        /// <summary>
        /// Cierra la conexion a la base de datos
        /// </summary>
        /// <param name="rollbackTransaction">
        /// indica si debe deshacerse la transaccion pendiente, en caso contrario, la misma no se altera
        /// </param>
        /// <returns>True si el proceso retorna exitosamente</returns>
        public void Close(bool rollbackTransaction = false)
        {
            Debug.WriteLine($"closing connection, rollback: { rollbackTransaction}");
            Debug.WriteLine($"conn != null: { conn != null}");

            if (conn != null)
            {
                Debug.WriteLine($"conn id: { conn.GetHashCode()}");
                Debug.WriteLine($"conn state: { conn.State.ToString()}");

                if (conn.State == ConnectionState.Open)
                {
                    if (rollbackTransaction)
                    {
                        RollbackTransaction();

                        Debug.WriteLine($"rolling back tran");
                    }
                    else
                    {
                        if (transaction != null)
                        {
                            transaction.Commit();
                            transactionWasCommited = true;
                        }
                    }

                    conn.Close();
                    conn.Disposed -= OnConnDisposed;
                    isConnDisposed = false;

                    Debug.WriteLine($"closed conn, and removed on disposed event ");
                }
            }
        }

        /// <summary>
        /// Crea un comando y lo establece como el tipo especificado y con el timeout especificado
        /// </summary>
        /// <param name="NombreSP">Indica el nombre del stored procedure</param>
        /// <param name="TipoComando">Indica el tipo del comando</param>
        /// <param name="TimeOut">Indica el timeout del comando</param>

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
            if (!IsDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                IsDisposed = true;
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL retornando la cantidad de filas afectadas
        /// </summary>
        /// <returns>Retorna el nro de filas afectadas</returns>
        public int ExecuteNonQueryCommand()
        {
            return (int)ExecuteCommand(CommandReturnType.NonQuery);
        }

        /// <summary>
        /// Ejecuta un comando SQL retornando el DataTable correspondiente
        /// </summary>
        /// <returns>Retorna el DataTable obtenido</returns>
        public DataTable ExecuteReaderCommand()
        {
            return (DataTable)ExecuteCommand(CommandReturnType.Reader);
        }

        /// <summary>
        /// Ejecuta un comando SQL retornando el escalar correspondiente
        /// </summary>
        /// <returns>Retorna el valor escalar obtenido</returns>
        public object ExecuteScalarCommand()
        {
            return ExecuteCommand(CommandReturnType.Scalar);
        }

        public bool Open(bool beginTransaction = false)
        {
            bool bReturn = false;

            try
            {
                Debug.WriteLine($"Opening connection, conn ==null: { conn == null}");

                if (conn == null)
                    CreateConnection();

                Debug.WriteLine($"conn state: { conn.State.ToString()}");

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                Debug.WriteLine($"conn state: { conn.State.ToString()}");
                Debug.WriteLine($"conn id: { ConnHash}");

                Debug.WriteLine($"begin tran: { beginTransaction}");
                if (beginTransaction)
                    transaction = conn.BeginTransaction();

                bReturn = true;
            }
            catch
            {
                Debug.WriteLine($"error on dbadoconnection conn!=null: { conn != null}");
                if (conn != null)
                    conn.Dispose();

                conn = null;
                throw;
            }

            return bReturn;
        }

        protected internal abstract void AddParameter(string Nombre, object Valor, ParameterDirection Direccion, DbType Tipo, int size);

        protected internal abstract int ExecuteNonQuery();

        protected internal abstract DataTable ExecuteReader();

        protected internal abstract object ExecuteScalar();

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //managed
                dbProviderFactory = null;

                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn.Disposed -= OnConnDisposed;
                }

                if (transaction != null)
                {
                    if (!transactionWasCommited)
                        transaction.Rollback();

                    transaction.Dispose();
                }

                if (command != null)
                    command.Dispose();
            }

            //unmanaged
        }

        /// <summary>
        /// Convierte un DataReader en un DataTable
        /// </summary>
        /// <param name="reader">DataReader a convertir</param>
        /// <param name="destroyreader">Indica si el reader debe cerrarse y destruirse</param>
        /// <returns>Data table convertido</returns>
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

        /// <summary>
        /// Crea una conexion a la base de datos utilizando el Connection String especificado en la
        /// creacion de esta clase
        /// </summary>
        private void CreateConnection()
        {
            conn = dbProviderFactory.CreateConnection();
            conn.ConnectionString = connstring;
            conn.Disposed += OnConnDisposed;

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
            object retVal = null;
            var closeConn = false;

            try
            {
                closeConn = PrepareConnAndTranForExecution();
                retVal = DoExecuteCommand(returnType);
            }
            catch
            {
                //controlar la excep, cerrando la conn segun lo especificado,y siempre deshacer la transaccion
                ManageConnectionAndTransactionOnException(closeConn, true);
                throw;
            }
            finally
            {
                if (transaction != null)
                    transaction.Commit();
                
                if (closeConn)
                    Close(false);//cerrar la conn sin tocar la tran
            }
            return retVal;
        }

        /// <summary>
        /// Arroja una excepcion capturada y deshace una transaccion tambien cierra la conexion en
        /// caso de ser indicado
        /// </summary>
        /// <param name="ex">Excepcion capturada</param>
        /// <param name="CloseConn">Indica si debe cerrarse la conexion</param>
        /// <param name="UndoTran">Indica si debe deshacerse la transaccion</param>
        private void ManageConnectionAndTransactionOnException(bool CloseConn, bool UndoTran)
        {
            if (CloseConn)
                Close(UndoTran);
        }

        private void OnConnDisposed(object sender, EventArgs e)
        {
            isConnDisposed = true;
            Debug.WriteLine($"Disposed internal Connection {((DbConnection)sender).GetHashCode()}");
        }

        private bool PrepareConnAndTranForExecution()
        {
            bool closeConn = false;

            if (transaction != null)//si existe una transac asignarla al comando
                command.Transaction = transaction;
            else
            {
                if (conn.State == ConnectionState.Closed)//sino checkear el estado de la conn
                {
                    Open(false);
                    closeConn = true;//indicar que se debe cerrar la conn
                }
            }
            return closeConn;
        }

        /// <summary>
        /// Deshace la transaccion actual pendiente ejecutando un RollBack
        /// </summary>
        /// <returns>True si el comando se ejecuta correctamente</returns>
        private void RollbackTransaction()
        {
            if (transaction != null)
                transaction.Rollback();
        }

        ~DbAdoConnection()
        {
            Dispose(false);
        }
    }
}
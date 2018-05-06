using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace JP.Base.DAL.ADO.ConnectionManagement
{
    /// <summary>
    /// Define los comportamientos comunes a todas las conexiones a bases de datos que se
    /// utilizan en este ensamblado
    /// </summary>
    internal abstract class DBCommonConnection : IDBAdoConnection, IDisposable
    {
        
        protected DbCommand _DBCmd = null;

        protected DbConnection _DBConn = null;

        protected DbTransaction _DBTran = null;

        protected DbProviderFactory _ProviderFactory = null;

        private string _ConnString = string.Empty;

        private string _DataProvider = string.Empty;

        private bool _Disposed;

        /// <summary>
        /// Inicializa el objeto con los valores para el DataProvider y el ConnectionString desde el archivo Web.configo o App.config
        /// </summary>
        public DBCommonConnection()
        {
            _DataProvider = ConfigurationSettings.AppSettings["DataProvider"];
            _ConnString = ConfigurationSettings.AppSettings["ConnectionString"];
            _ProviderFactory = DbProviderFactories.GetFactory(_DataProvider);
        }

        /// <summary>
        /// Inicializa el objeto con los valores para el DataProvider y el ConnectionString definidos en los parametros
        /// </summary>
        /// <param name="DataProvider">Define el proveedor de datos de acuerdo al NameSpace del mismo. Ej: System.Data.SqlClient</param>
        /// <param name="ConnString">El string que representa la cadena de conexion a la base de datos</param>
        public DBCommonConnection(string DataProvider, string ConnString)
        {
            _ConnString = ConnString;
            _DataProvider = DataProvider;
            _ProviderFactory = DbProviderFactories.GetFactory(DataProvider);
        }

        ~DBCommonConnection()
        {
            Dispose(false);
        }

        internal enum TipoComandoRetorno
        {
            Escalar = 0,
            Tabular,
            Ninguno
        }

        public bool Abrir_Conexion(bool IniciarTransaccion)
        {
            bool bReturn = false;

            try
            {
                if (_DBConn == null)
                {
                    Crear_Conexion();
                }

                if (_DBConn.State == ConnectionState.Closed)
                {
                    _DBConn.Open();
                }

                if (IniciarTransaccion)
                {
                    _DBTran = _DBConn.BeginTransaction();
                }

                bReturn = true;
            }
            catch
            {
                _DBConn = null;
                throw;
            }

            return bReturn;
        }

        /// <summary>
        /// Abre una conexion a la base de datos especificada
        /// </summary>
        /// <param name="IniciarTransaccion">Indica si la apertura de la conexion implica iniciar una transaccion</param>
        /// <returns>True si la conexion se abrio exitosamente</returns>
        /// <summary>
        /// Abre una conexion a la base de datos especificada sin iniciar una transaccion
        /// </summary>
        /// <returns>True si la conexion se abrio exitosamente</returns>
        public bool Abrir_Conexion()
        {
            return Abrir_Conexion(false);
        }

        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y establece los valores null del parametro si
        /// el parametro valor es un String.Empty o un DateTime.MinValue o null
        /// </summary>
        /// <param name="Parametro">Nombre, valor y direccion del parametro</param>
        public void Agregar_Parametro_A_Comando(ParameterData Parametro)
        {
            if (Parametro.ValorParametro == null)
            {
                Agregar_Parametro(Parametro.NombreParametro, DBNull.Value, Parametro.DireccionParametro, Parametro.Tipo, Parametro.Size);
            }
            else
            {
                Agregar_Parametro(Parametro.NombreParametro, Parametro.ValorParametro, Parametro.DireccionParametro, Parametro.Tipo, Parametro.Size);

                if ((_DBCmd.Parameters[_DBCmd.Parameters.Count - 1].DbType == DbType.String) |
                        (_DBCmd.Parameters[_DBCmd.Parameters.Count - 1].DbType == DbType.AnsiString))
                {
                    if (string.IsNullOrEmpty(Parametro.ToString()))
                    {
                        _DBCmd.Parameters[_DBCmd.Parameters.Count - 1].Value = DBNull.Value;
                    }
                }
                else if (((_DBCmd.Parameters[_DBCmd.Parameters.Count - 1].DbType == DbType.Date) |
                                    (_DBCmd.Parameters[_DBCmd.Parameters.Count - 1].DbType == DbType.DateTime)) &&
                                    (Convert.ToDateTime(Parametro.ValorParametro, CultureInfo.InvariantCulture) == DateTime.MinValue))
                {
                    _DBCmd.Parameters[_DBCmd.Parameters.Count - 1].Value = DBNull.Value;
                }
            }
        }

        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y establece los valores null del parametro si
        /// el parametro valor es un String.Empty o un DateTime.MinValue o null
        /// </summary>
        /// <param name="Parametros">Lista de datos del parametro como nombre, valor y direccion</param>
        public void Agregar_Parametros_A_Comando(ref List<ParameterData> Parametros)
        {
            if (Parametros != null)
            {
                foreach (ParameterData Param in Parametros)
                {
                    Agregar_Parametro_A_Comando(Param);
                }
            }
        }

        /// <summary>
        /// Cierra la conexion a la base de datos
        /// </summary>
        /// <param name="DeshacerTransaccion">indica si debe deshacerse la transaccion pendiente, en caso contrario, la misma no se altera</param>
        /// <returns>True si el proceso retorna exitosamente</returns>
        public bool Cerrar_Conexion(bool DeshacerTransaccion)
        {
            bool bReturn = false;

            try
            {
                if (_DBConn != null)
                {
                    if (_DBConn.State == ConnectionState.Open)
                    {
                        if (DeshacerTransaccion)
                        {
                            Deshacer_Transaccion();
                        }
                        else
                        {
                            if (_DBTran != null)
                            {
                                _DBTran.Commit();
                            }
                        }

                        _DBConn.Close();
                    }
                }

                bReturn = true;
            }
            catch
            {
                throw;
            }

            return bReturn;
        }

        /// <summary>
        /// Cierra la conexion a la base de datos sin alterar la transaccion
        /// </summary>
        /// <returns>True si el proceso finaliza exitosamente</returns>
        public bool Cerrar_Conexion()
        {
            return Cerrar_Conexion(false);
        }

        /// <summary>
        /// Crea un comando y lo establece como el tipo especificado y con el timeout especificado
        /// </summary>
        /// <param name="NombreSP">Indica el nombre del stored procedure</param>
        /// <param name="TipoComando">Indica el tipo del comando</param>
        /// <param name="TimeOut">Indica el timeout del comando</param>
        public void Crear_Comando(string NombreCMD, CommandType TipoComando, int TimeOut)
        {
            if (_DBCmd!=null)
            {
                _DBCmd.Dispose();
                _DBCmd = null;
            }

            _DBCmd = _DBConn.CreateCommand();
            _DBCmd.CommandType = TipoComando;
            _DBCmd.CommandText = NombreCMD;
            _DBCmd.CommandTimeout = TimeOut;
        }

        /// <summary>
        /// Crea un comando y lo establece como el tipo especificado el timeout es 0
        /// </summary>
        /// <param name="NombreSP">Indica el nombre del stored procedure</param>
        /// <param name="TipoComando">Indica el tipo del comando</param>
        public void Crear_Comando(string NombreCMD, CommandType TipoComando)
        {
            Crear_Comando(NombreCMD, TipoComando, 0);
        }

        /// <summary>
        /// Crea un comando y lo establece como SP, el timeout del comando se establece en 0 y el tipo de comando es Stored Procedure
        /// </summary>
        /// <param name="NombreSP">Indica el nombre del stored procedure</param>
        public void Crear_Comando(string NombreCMD)
        {
            Crear_Comando(NombreCMD, CommandType.StoredProcedure, 0);
        }

        /// <summary>
        /// Deshace la transaccion actual pendiente ejecutando un RollBack
        /// </summary>
        /// <returns>True si el comando se ejecuta correctamente</returns>
        public bool Deshacer_Transaccion()
        {
            bool bReturn = false;

            try
            {
                if (_DBTran != null)
                {
                    _DBTran.Rollback();
                }

                bReturn = true;
            }
            catch
            {
                throw;
            }

            return bReturn;
        }

        public void Dispose()
        {
            if (!_Disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                _Disposed = true;
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL retornando el escalar correspondiente
        /// </summary>
        /// <returns>Retorna el valor escalar obtenido</returns>
        public object Ejecutar_Comando_De_Retorno_Escalar()
        {
            return Ejecutar_Comando(TipoComandoRetorno.Escalar);
        }

        /// <summary>
        /// Ejecuta un comando SQL retornando el DataTable correspondiente
        /// </summary>
        /// <returns>Retorna el DataTable obtenido</returns>
        public DataTable Ejecutar_Comando_De_Retorno_Tabular()
        {
            return (DataTable)Ejecutar_Comando(TipoComandoRetorno.Tabular);
        }

        /// <summary>
        /// Ejecuta un comando SQL retornando la cantidad de filas afectadas
        /// </summary>
        /// <returns>Retorna el nro de filas afectadas</returns>
        public int Ejecutar_Comando_Sin_Retorno()
        {
            return (int)Ejecutar_Comando(TipoComandoRetorno.Ninguno);
        }

        /// <summary>
        /// Arroja una excepcion capturada y deshace una transaccion tambien cierra la conexion en caso de ser indicado
        /// </summary>
        /// <param name="ex">Excepcion capturada</param>
        /// <param name="CloseConn">Indica si debe cerrarse la conexion</param>
        /// <param name="UndoTran">Indica si debe deshacerse la transaccion</param>
        public void Handle_Exception(bool CloseConn, bool UndoTran)
        {
            if (CloseConn)
            {
                Cerrar_Conexion(UndoTran);
            }
        }

        protected internal abstract void Agregar_Parametro(string Nombre, object Valor, ParameterDirection Direccion, DbType Tipo, int size);

        protected internal abstract object Ejecutar_CMD_Escalar();

        protected internal abstract int Ejecutar_CMD_Sin_Retorno();

        protected internal abstract DataTable Ejecutar_CMD_Tabular();

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //managed
                _ProviderFactory = null;

                if (_DBConn != null)
                {
                    _DBConn.Close();
                    _DBConn.Dispose();
                }

                if (_DBTran != null)
                {
                    _DBTran.Rollback();
                    _DBTran.Dispose();
                }

                if (_DBCmd != null)
                    _DBCmd.Dispose();
            }

            //unmanaged
        }

        /// <summary>
        /// Convierte un DataReader en un DataTable
        /// </summary>
        /// <param name="Reader">DataReader a convertir</param>
        /// /// <param name="DestroyReader">Indica si el reader debe cerrarse y destruirse</param>
        /// <returns>Data table convertido</returns>
        protected DataTable Reader_To_Table(ref DbDataReader Reader, bool DestroyReader)
        {
            DataTable DTReturn = new DataTable();
            DataReaderAdapter ReaderAdapter = new DataReaderAdapter();
            DTReturn.Locale = CultureInfo.CurrentCulture;

            try
            {
                ReaderAdapter.Fill_From_Reader(DTReturn, Reader);

                if (DestroyReader)
                {
                    Reader.Close();
                    ReaderAdapter.Dispose();
                }
            }
            catch
            {
                throw;
            }

            return DTReturn;
        }

        /// <summary>
        /// Crea una conexion a la base de datos utilizando el Connection String especificado en la creacion de esta clase
        /// </summary>
        private void Crear_Conexion()
        {
            _DBConn = _ProviderFactory.CreateConnection();
            _DBConn.ConnectionString = _ConnString;
        }

        private object Ejecutar_CMD(TipoComandoRetorno TipoRetorno, object oReturnVal)
        {
            switch (TipoRetorno)
            {
                case TipoComandoRetorno.Escalar:
                    oReturnVal = Ejecutar_CMD_Escalar();
                    break;

                case TipoComandoRetorno.Tabular:
                    oReturnVal = Ejecutar_CMD_Tabular();
                    break;

                case TipoComandoRetorno.Ninguno:
                    oReturnVal = Ejecutar_CMD_Sin_Retorno();
                    break;
            }
            return oReturnVal;
        }

        private object Ejecutar_Comando(TipoComandoRetorno TipoRetorno)
        {
            object oReturnVal = null;
            bool bCerrarConn = false;

            try
            {
                bCerrarConn = Preparar_Conexion_Y_Transaccion_Para_Ejecucion();

                oReturnVal = Ejecutar_CMD(TipoRetorno, oReturnVal);
            }
            catch
            {
                //controlar la excep, cerrando la conn segun lo especificado,y siempre deshacer la transaccion
                Handle_Exception(bCerrarConn, true);
                throw;
            }
            finally
            {
                if (bCerrarConn)
                {
                    Cerrar_Conexion(false);//cerrar la conn sin tocar la tran
                }
            }
            return oReturnVal;
        }

        private bool Preparar_Conexion_Y_Transaccion_Para_Ejecucion()
        {
            bool bCerrarConn = false;

            if (_DBTran != null)//si existe una transac asignarla al comando
            {
                _DBCmd.Transaction = _DBTran;
            }
            else
            {
                if (_DBConn.State == ConnectionState.Closed)//sino checkear el estado de la conn
                {
                    Abrir_Conexion(false);
                    bCerrarConn = true;//indicar que se debe cerrar la conn
                }
            }
            return bCerrarConn;
        }
    }
}
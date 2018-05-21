using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Connections;
using System;
using System.Configuration;
using System.Diagnostics;

namespace JP.Base.DAL.ADO.Implementations.Factories
{
    /// <summary>
    /// <para>v1.0.0 - 26-03-2010</para>
    /// <para>Clase estática que representa una fabrica de conexiones a base de datos</para>
    /// <para>Es posible seleccionar entre: Oracle, SQL Server y OleDb</para>
    /// </summary>
    public class DbConnFactory : IDbConnFactory
    {
        private IDbAdoConnection currentConnection;

        private string defaultedConnectionString;

        private string defaultedDataProvider;

        public DbConnFactory()
        {
        }

        public DbConnFactory(string dataProvider = "", string connectionString = "")
        {
            defaultedDataProvider = dataProvider;
            defaultedConnectionString = connectionString;
        }

        /// <summary>
        /// Devuelve IDBConnection que representa la conexion seleccionada segun los parametros proveídos
        /// </summary>
        /// <param name="dataProvider">Namespace del Data Provider <example>System.Data.OracleClient</example></param>
        /// <param name="ConnString">
        /// Connection String utilizada para conectar con la base de datos especificada
        /// </param>
        /// <returns>IDBConnection que representa la conexion seleccionada</returns>
        public IDbAdoConnection GetConnection(string dataProvider = "", string connectionString = "")
        {
            var pDataProv = "";
            if (string.IsNullOrEmpty(dataProvider) && string.IsNullOrEmpty(defaultedDataProvider))
                pDataProv = ConfigurationManager.AppSettings["DataProvider"];
            else
                pDataProv = string.IsNullOrEmpty(dataProvider) ? defaultedDataProvider : dataProvider;

            var pConnString = "";

            if (string.IsNullOrEmpty(connectionString) && string.IsNullOrEmpty(defaultedConnectionString))
                pConnString = ConfigurationManager.AppSettings["ConnectionString"];
            else
                pConnString = string.IsNullOrEmpty(connectionString) ? defaultedConnectionString : connectionString;

            return GetConnectionByProvider(pDataProv, pConnString);
        }

        /// <summary>
        /// Devuelve una instancia de un tipo de conexion a base de datos especifico, basado en los
        /// parametros proveídos
        /// </summary>
        /// <param name="DataProvider">Namespace del Data Provider <example>System.Data.OracleClient</example></param>
        /// <param name="ConnString">
        /// Connection String utilizada para conectar con la base de datos especificada
        /// </param>
        /// <returns>IDBConnection que representa la conexion seleccionada</returns>
        private IDbAdoConnection GetConnectionByProvider(string DataProvider, string ConnString)
        {
            if (currentConnection == null || currentConnection.IsDisposed)
            {
                IDbAdoConnection dbconn;

                if (DataProvider.Equals("System.Data.OracleClient", StringComparison.CurrentCultureIgnoreCase))
                    dbconn = new Oracle9DbConnection(DataProvider, ConnString);
                else if (DataProvider.Equals("System.Data.SqlClient", StringComparison.CurrentCultureIgnoreCase))
                    dbconn = new SqlServerDbConnection(DataProvider, ConnString);
                else
                    dbconn = new OleDbConnection(DataProvider, ConnString);

                Debug.WriteLine($"providing connection: {dbconn.ConnHash}");

                currentConnection = dbconn;
            }

            return currentConnection;
        }
    }
}
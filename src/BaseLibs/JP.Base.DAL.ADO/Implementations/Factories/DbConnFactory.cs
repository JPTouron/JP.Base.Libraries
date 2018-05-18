using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Connections;
using System;
using System.Configuration;

namespace JP.Base.DAL.ADO.Implementations.Factories
{
    /// <summary>
    /// <para>v1.0.0 - 26-03-2010</para>
    /// <para>Clase estática que representa una fabrica de conexiones a base de datos</para>
    /// <para>Es posible seleccionar entre: Oracle, SQL Server y OleDb</para>
    /// </summary>
    public  class DbConnFactory : IDbConnFactory
    {
        /// <summary>
        /// Devuelve IDBConnection que representa la conexion seleccionada segun los parametros proveídos
        /// </summary>
        /// <param name="dataProvider">Namespace del Data Provider <example>System.Data.OracleClient</example></param>
        /// <param name="ConnString">
        /// Connection String utilizada para conectar con la base de datos especificada
        /// </param>
        /// <returns>IDBConnection que representa la conexion seleccionada</returns>
        public  IDbAdoConnection GetConnection(string dataProvider = "", string connectionString = "")
        {
            if (string.IsNullOrEmpty(dataProvider))
                dataProvider = ConfigurationManager.AppSettings["DataProvider"];

            if (string.IsNullOrEmpty(connectionString))
                connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            return GetConnectionByProvider(dataProvider, connectionString);
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
        private  IDbAdoConnection GetConnectionByProvider(string DataProvider, string ConnString)
        {
            IDbAdoConnection IDBConn;

            if (DataProvider.Equals("System.Data.OracleClient", StringComparison.CurrentCultureIgnoreCase))
                IDBConn = new Oracle9DbConnection(DataProvider, ConnString);
            else if (DataProvider.Equals("System.Data.SqlClient", StringComparison.CurrentCultureIgnoreCase))
                IDBConn = new SqlServerDbConnection(DataProvider, ConnString);
            else
                IDBConn = new OleDbConnection(DataProvider, ConnString);

            return IDBConn;
        }
    }
}
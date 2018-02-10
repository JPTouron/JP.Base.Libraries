using System;
using System.Configuration;

namespace JP.Base.DataAccessModel.DBServices.ConnectionManagement
{
    /// <summary>
    /// <para>v1.0.0 - 26-03-2010</para>
    /// <para>
    /// Clase estática que representa una fabrica de conexiones a base de datos
    /// </para>
    /// <para>
    /// Es posible seleccionar entre: Oracle, SQL Server y OleDb
    /// </para>
    /// </summary>
    public static class DBConnFactory
    {
        /// <summary>
        /// Devuelve IDBConnection que representa la conexion seleccionada segun los parametros proveídos
        /// </summary>
        /// <param name="DataProvider">Namespace del Data Provider <example>System.Data.OracleClient</example></param>
        /// <param name="ConnString">Connection String utilizada para conectar con la base de datos especificada</param>
        /// <returns>IDBConnection que representa la conexion seleccionada</returns>
        public static IDBConnection Obtener_Nueva_Conexion(string DataProvider, string ConnectionString)
        {
            return Instanciar_Conexion_SegunBaseDeDatos(DataProvider, ConnectionString);
        }

        /// <summary>
        /// Devuelve IDBConnection que representa la conexion seleccionada segun las configuraciones especificadas en la seccion appSettings del archivo .config de la aplicacion
        /// <example><para>-</para><para>Ejemplo: (cambiar corchetes por simbolos mayor/menor)</para><para>-</para>
        /// <code>
        ///	<para>[configuration]</para>
        ///	<para>[appSettings]</para>
        ///	<para>[add key="ConnectionString" value="Mi_Connection_String"/]</para>
        ///	<para>[add key="DataProvider" value="System.Data.SqlClient"/]</para>
        ///	<para>[/appSettings]</para>
        ///	<para>[/configuration]</para>
        /// </code>
        ///	</example>
        /// </summary>
        /// <remarks><para>Obtiene los datos de DataProvider y Connection String desde la seccion appSettings del archivo .config de la aplicacion</para>
        /// </remarks>
        /// <returns>IDBConnection que representa la conexion seleccionada</returns>
        public static IDBConnection Obtener_Nueva_Conexion()
        {
            return Instanciar_Conexion_SegunBaseDeDatos(ConfigurationSettings.AppSettings["DataProvider"], ConfigurationSettings.AppSettings["ConnectionString"]);
        }

        /// <summary>
        /// Devuelve una instancia de un tipo de conexion a base de datos especifico, basado en los parametros proveídos
        /// </summary>
        /// <param name="DataProvider">Namespace del Data Provider <example>System.Data.OracleClient</example></param>
        /// <param name="ConnString">Connection String utilizada para conectar con la base de datos especificada</param>
        /// <returns>IDBConnection que representa la conexion seleccionada</returns>
        private static IDBConnection Instanciar_Conexion_SegunBaseDeDatos(string DataProvider, string ConnString)
        {
            IDBConnection IDBConn;

            if (DataProvider.Equals("System.Data.OracleClient", StringComparison.CurrentCultureIgnoreCase))
            {
                IDBConn = new Oracle9DBConnection(DataProvider, ConnString);
            }
            else if (DataProvider.Equals("System.Data.SqlClient", StringComparison.CurrentCultureIgnoreCase))
            {
                IDBConn = new SQLServerDBConnection(DataProvider, ConnString);
            }
            else
            {
                IDBConn = new OleDbConnection(DataProvider, ConnString);
            }

            return IDBConn;
        }
    }
}
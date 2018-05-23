using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Connections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace JP.Base.DAL.ADO.Implementations.Factories
{
    /// <summary>
    /// provides an implementation of the IDbAdoConnection based on the dataprovider set as parameter
    /// </summary>
    public class DbConnFactory : IDbConnFactory
    {
        private IDictionary<string, Func<string, string, IDbAdoConnection>> connectionsByProviders;
        private IDbAdoConnection currentConnection;

        private string defaultedConnectionString;

        private string defaultedDataProvider;

        public DbConnFactory()
        {
            connectionsByProviders = new Dictionary<string, Func<string, string, IDbAdoConnection>>();

            SetupProviderDictionary(ref connectionsByProviders);
        }

        public DbConnFactory(string dataProvider = "", string connectionString = "")
        {
            defaultedDataProvider = dataProvider;
            defaultedConnectionString = connectionString;
        }

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
        /// when overridden we can add (or re-create) the dictionary that would pair dataproviders as string with a method to create IDbAdoConnection implementation
        /// </summary>
        protected virtual void SetupProviderDictionary(ref IDictionary<string, Func<string, string, IDbAdoConnection>> dbConnectionsByProviders)
        {
            dbConnectionsByProviders.Add("System.Data.OracleClient", (dataProvider, connString) => new Oracle9DbConnection(dataProvider, connString));
            dbConnectionsByProviders.Add("System.Data.SqlClient", (dataProvider, connString) => new SqlServerDbConnection(dataProvider, connString));
            dbConnectionsByProviders.Add("System.Data.OleDb", (dataProvider, connString) => new SqlServerDbConnection(dataProvider, connString));
        }

        private IDbAdoConnection GetConnectionByProvider(string dataProvider, string connString)
        {
            if (currentConnection == null || currentConnection.IsDisposed)
            {
                if (connectionsByProviders.ContainsKey(dataProvider))
                {
                    currentConnection = connectionsByProviders[dataProvider].Invoke(dataProvider, connString);
                    Debug.WriteLine($"providing connection: {currentConnection.ConnHash}");
                }
                else
                    currentConnection = new OleDbConnection(dataProvider, connString);
            }

            return currentConnection;
        }
    }
}
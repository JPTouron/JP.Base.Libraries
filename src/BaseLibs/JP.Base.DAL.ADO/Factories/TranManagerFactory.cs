using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.TransactionManagement;
using System.Configuration;

namespace JP.Base.DAL.ADO.Factories
{
    public static class TranManagerFactory
    {
        public static ITransactionManager Obtener_Nuevo_Connection_Manager(string DataProvider, string ConnectionString)
        {
            return Instanciar_ConnManager(DataProvider, ConnectionString);
        }

        public static ITransactionManager Obtener_Nuevo_Connection_Manager()
        {
            return Instanciar_ConnManager(ConfigurationManager.AppSettings["DataProvider"], ConfigurationManager.AppSettings["ConnectionString"]);
        }

        private static ITransactionManager Instanciar_ConnManager(string DataProvider, string ConnString)
        {
            return new TransactionManager(DataProvider, ConnString); ;
        }
    }
}
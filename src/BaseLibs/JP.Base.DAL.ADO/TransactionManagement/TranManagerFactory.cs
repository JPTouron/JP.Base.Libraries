using System.Configuration;

namespace JP.Base.DAL.ADO.TransactionManagement
{
    public static class TranManagerFactory
    {
        public static ITransactionManager Obtener_Nuevo_Connection_Manager(string DataProvider, string ConnectionString)
        {
            return Instanciar_ConnManager(DataProvider, ConnectionString);
        }

        public static ITransactionManager Obtener_Nuevo_Connection_Manager()
        {
            return Instanciar_ConnManager(ConfigurationSettings.AppSettings["DataProvider"], ConfigurationSettings.AppSettings["ConnectionString"]);
        }

        private static ITransactionManager Instanciar_ConnManager(string DataProvider, string ConnString)
        {
            return new TransactionManager(DataProvider, ConnString); ;
        }
    }
}
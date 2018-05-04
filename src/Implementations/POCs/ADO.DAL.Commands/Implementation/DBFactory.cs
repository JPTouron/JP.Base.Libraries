using ADO.DAL.Commands.Contracts;

namespace ADO.DAL.Commands
{
    public static class DBFactory
    {
        public static IDbAccess GetDBAccess()
        {

            var db = new DbAccess();

            return db;
        }
    }
}
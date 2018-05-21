namespace JP.Base.DAL.ADO.Contracts
{
    public interface IDbConnFactory
    {
        IDbAdoConnection GetConnection(string dataProvider = "", string connectionString = "");
    }
}
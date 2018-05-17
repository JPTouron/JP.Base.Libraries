namespace JP.Base.DAL.ADO.Contracts
{
    public interface ITransactionManager
    {
        int BeginTransaction(out IDbAdoConnection conn);

        void FinishTransaction(int transactionId, bool rollbackTransaction = false);

        IDbAdoConnection GetConnection(int transactionId = 0);
    }
}
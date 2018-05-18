using JP.Base.DAL.UnitOfWork;

namespace JP.Base.DAL.ADO.UnitOfWork.Contracts
{
    public interface IBaseUnitOfWorkAdo : IBaseUnitOfWork
    {
        IExecutionData ExecutionData { get; }
    }
}
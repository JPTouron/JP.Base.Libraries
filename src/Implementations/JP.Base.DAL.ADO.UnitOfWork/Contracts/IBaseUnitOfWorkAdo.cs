using JP.Base.DAL.ADO.Repositories;
using JP.Base.DAL.UnitOfWork;

namespace JP.Base.DAL.ADO.UnitOfWork.Contracts
{
    public interface IBaseUnitOfWorkAdo : IBaseUnitOfWork
    {
        IExecutionData ExecutionData { get; }

        /// <summary>
        /// obtains a <see cref="IGenericRepository{TEntity}"/> to operate the underlying DbSet
        /// </summary>
        /// <typeparam name="TModel">The model the repository handles</typeparam>
        IGenericRepository<TModel> GetGenericRepo<TModel>() where TModel : class;
    }
}
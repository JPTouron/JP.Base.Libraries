using JP.Base.DAL.EF6.Repositories.Contracts;
using JP.Base.DAL.UnitOfWork;

namespace JP.Base.DAL.EF6.UnitOfWork
{
    /// <summary>
    /// models a unit of work, in which we manage the life time of a db context to perform database operations
    /// </summary>
    public interface IBaseUnitOfWorkEf : IBaseUnitOfWork
    {
        /// <summary>
        /// obtains a <see cref="IGenericRepository{TEntity}"/> to operate the underlying DbSet
        /// </summary>
        /// <typeparam name="TModel">The model the repository handles</typeparam>
        IGenericRepository<TModel> GetGenericRepo<TModel>() where TModel : class;

        TRepo GetSpecificRepo<TRepo>() where TRepo : class;

    }
}
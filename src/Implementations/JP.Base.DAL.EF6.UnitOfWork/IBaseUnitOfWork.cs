using JP.Base.DAL.EF6.Repositories.Contracts;
using System;

namespace JP.Base.DAL.EF6.UnitOfWork
{
    /// <summary>
    /// models a unit of work, in which we manage the life time of a db context to perform database operations
    /// </summary>
    public interface IBaseUnitOfWork : IDisposable
    {
        bool IsDisposed { get; }

        /// <summary>
        /// allows to execute a method against the db, designed for insert,update or delete
        /// operations (or even basic search)
        /// </summary>
        /// <param name="meth">
        /// method that should be called by the unit of work and that contains the logic to run in
        /// the database
        /// </param>
        /// <param name="saveChanges">
        /// whether or not to save changes made to the database by the invoked method
        /// </param>
        void Execute(Action meth, bool saveChanges = false);

        /// <summary>
        /// allows to execute a function against the db, designed for user-defined actions such as
        /// specific search
        /// </summary>
        /// <typeparam name="TResult">the result returned by the function</typeparam>
        /// <param name="meth">the function to run against the db</param>
        /// <returns>whatever you told it would return</returns>
        TResult Execute<TResult>(Func<TResult> meth);

        /// <summary>
        /// allows to execute a function with a specifically defined parameter against the database
        /// </summary>
        TResult Execute<TIn, TResult>(Func<TIn, TResult> meth, TIn arg);

        /// <summary>
        /// obtains a <see cref="IGenericRepositoryEf{TEntity}"/> to operate the underlying DbSet
        /// </summary>
        /// <typeparam name="TModel">The model the repository handles</typeparam>
        IGenericRepositoryEf<TModel> GetGenericRepo<TModel>() where TModel : class;
    }
}
using JP.Base.DAL.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace JP.Base.DAL.EF6.Repositories.Contracts
{
    /// <summary>
    /// defines base actions to perform in a repository
    /// </summary>
    /// <typeparam name="TEntity">the entity that your repo should implement</typeparam>
    public interface IGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// the dbset representing the set from <see cref="TEntity"/> obtained from the <seealso cref="DbContext"/>
        /// </summary>
        DbSet<TEntity> dbSet { get; }

        /// <summary>
        /// attaches an entity to your <see cref="DbContext"/> context
        /// <para>
        /// <param name="entity">the entity you need to be attached</param>
        void AttachEntity(TEntity entity);

        /// <summary>
        /// provides a way to obtain a collection out of the dbSet by means of a query
        /// </summary>
        /// <param name="filter"> an expression that would equal the Where part of a query</param>
        /// <param name="orderBy">the order by function to apply to the dbset</param>
        /// <param name="includeNavigationProperties">comma separated values with the property names of the navigation properties to traverse for information, equals to an eager-loading mode</param>
        /// <param name="skip">pagination tool which records to skip before we start querying the db</param>
        /// <param name="take">pagination tool equals TOP from Sql</param>
        /// <param name="dontTrack">if true the returned entities are not tracked by the DbContext, this is useful when executing queries for read-only purposes</param>
        /// <returns>the IQueryable{TEntity} with the results obtained from the query or null if no results were found</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeNavigationProperties = "", int skip = 0, int take = 0, bool dontTrack = true);

        /// <summary>
        /// Verifies wether the entity has an unchanged state
        /// </summary>
        bool IsEntityUnchanged(TEntity entity);
    }
}
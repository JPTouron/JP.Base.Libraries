using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace JP.Base.DAL.EF6.Contracts
{
    /// <summary>
    /// models a <seealso cref="System.Data.Entity.DbContext"/>, exposing several useful actions that are most used to work with data accessing in the EF6 model
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// indicates if this context has been disposed
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// invokes the <seealso cref="DbContext.Entry(object)"/> method
        /// </summary>
        DbEntityEntry Entry(object entity);

        /// <summary>
        /// checks wether an entity is detached from the context, based on the <seealso cref="DbContext.Entry(object)"/>  method
        /// </summary>
        /// <param name="entity">the entity you wish to verify</param>
        /// <returns>true if the <see cref="DbEntityEntry"/> has a <see cref="EntityState.Detached"/> state, false otherwise</returns>
        bool IsEntityDetached(object entity);

        /// <summary>
        /// checks wether an entity is State is unchanged, based on the <seealso cref="DbContext.Entry(object)"/>  method
        /// </summary>
        /// <param name="entity">the entity you wish to verify</param>
        /// <returns>true if the <see cref="DbEntityEntry"/> has a <see cref="EntityState.Unchanged"/> state, false otherwise</returns>
        bool IsEntityUnchanged(object entity);

        /// <summary>
        /// invokes the <see cref="DbContext.SaveChanges"/> method
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// invokes the <seealso cref="DbContext.Set{TEntity}"/> method
        /// </summary>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// checks wether an entity is State is modified, based on the <seealso cref="DbContext.Entry(object)"/>  method
        /// </summary>
        /// <param name="entity">the entity you wish to verify</param>
        /// <returns>true if the <see cref="DbEntityEntry"/> has a <see cref="EntityState.Modified"/> state, false otherwise</returns>
        void SetEntityAsModified(object entity);
    }
}
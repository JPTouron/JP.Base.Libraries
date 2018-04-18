using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JP.Base.DAL.EF6.Contracts;
using JP.Base.DAL.EF6.Repositories.Contracts;
using JP.Base.DAL.EF6.Repositories.Exceptions;

namespace JP.Base.DAL.EF6.Repositories.Implementation
{
    /// <summary>
    /// provides an implementation of <see cref="IGenericRepository{TEntity}"/>
    /// <para>you are not required to base your implementation on this</para>
    /// </summary>
    /// <typeparam name="TEntity">the type of the entity to be managed by this repository</typeparam>
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// a simple factory that would get you the current context instance to be used within this repo
        /// </summary>
        private IContextProvider factory;

        public GenericRepository(IContextProvider factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// the dbset representing the set from <see cref="TEntity"/> obtained from the <seealso cref="DbContext"/>
        /// </summary>
        public DbSet<TEntity> dbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        /// <summary>
        /// the underlying <see cref="IDbContext"/> context that the dbset of this repository is linked to
        /// </summary>
        protected IDbContext context
        {
            get
            {
                return factory.ProvideContext();
            }
        }

        /// <summary>
        /// this method attaches an entity to your <see cref="DbContext"/> context by use of <see cref="DbSet.Attach(object)"/> method
        /// <para>
        /// SUGGESTION:
        /// <para>you could use this code in your derived implementation of the <see cref="GenericRepository{TEntity}"/> to handle an invalid operation exception:
        /// <para>
        /// <code>
        /// <para>
        /// throw an update exception as it might have to do with invalid ids on entities linked
        /// with relationship to others while being attached
        /// </para>
        /// var msg = new stringbuilder();
        ///
        /// if (entity != null)
        /// {
        ///     var entityname = entity.gettype().name.tostring();
        ///     var props = entity.gettype().getproperties().tolist();
        ///
        ///     msg.appendline(string.format("failing entity type name: {0}", entityname));
        ///     msg.appendline("");
        ///     msg.appendline("properties values:");
        ///     props.foreach (x =>
        ///     {
        ///         var propval = x.getvalue(entity);
        ///         var propvalstring = propval != null ? propval.tostring() : "null";
        ///         msg.appendline(string.format("{0} = {1}", x.name, propvalstring));
        ///     });
        /// }
        /// <para>
        /// throw new SOME_CUSOMIZED_EXCEPTION(msg.tostring(), ex, entity);
        /// </para>
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="entity">the entity you need to be attached</param>
        public virtual void AttachEntity(TEntity entity)
        {
            dbSet.Attach(entity);
        }

        /// <summary>
        /// given an <see cref="TEntity"/> type, this method invokes <see cref="DbSet.Remove(object)"/>  method
        /// <para>if the entity is not attached to context then it attaches it through the <see cref="AttachEntity(TEntity)"/> method</para>
        /// </summary>
        /// <param name="entityToDelete">the <see cref="TEntity"/> object you wish to delete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.IsEntityDetached(entityToDelete))
                AttachEntity(entityToDelete);

            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// finds an entity through <see cref="DbSet.Find(object[])"/> and invokes <see cref="Delete(TEntity)"/> method to delete it
        /// </summary>
        /// <exception cref="EntityToDeleteIsNullException">thrown when entity is not found</exception>
        /// <param name="id">the id of the entity that you wish to delete</param>
        public virtual void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);

            if (entityToDelete == null)
                throw new EntityToDeleteIsNullException(id);

            Delete(entityToDelete);
        }

        /// <summary>
        /// provides a way to obtain a collection out of the dbSet by means of a query
        /// </summary>
        /// <param name="filter"> an expression that would equal the Where part of a query</param>
        /// <param name="orderBy">the order by function to apply to the dbset</param>
        /// <param name="includeNavigationProperties">comma separated values with the property names of the navigation properties to traverse for information, equals to an eager-loading mode</param>
        /// <param name="skip">pagination tool which records to skip before we start querying the db</param>
        /// <param name="take">pagination tool equals TOP from Sql</param>
        /// <param name="dontTrack">if true the dbSet method <see cref="System.Data.Entity.Infrastructure.DbQuery{TResult}.AsNoTracking"/> is invoked and the returned entities are not tracked by the DbContext, this is useful when executing queries for read-only purposes</param>
        /// <returns>the IQueryable{TEntity} with the results obtained from the query or null if no results were found</returns>
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeNavigationProperties = "",
            int skip = 0,
            int take = 0,
            bool dontTrack = true)
        {
            IQueryable<TEntity> query;

            if (dontTrack)
                query = dbSet.AsNoTracking();
            else
                query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeNavigationProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            
            return query;
        }

        /// <summary>
        /// returns an entity of type TEntity based on id by executing the <seealso cref="DbSet.Find(object)"/> method
        /// </summary>
        /// <param name="id">the id used to identify the entity</param>
        /// <returns>the entity found or null</returns>
        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// inserts the entity into the underlying dbset
        /// </summary>
        /// <param name="entity">entity to be inserted</param>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// <seealso cref="IDbContext.IsEntityUnchanged(object)"/>
        /// </summary>
        public virtual bool IsEntityUnchanged(TEntity entity)
        {
            return context.IsEntityUnchanged(entity);
        }

        /// <summary>
        /// invokes the <see cref="AttachEntity(TEntity)"/> method and the <see cref="IDbContext.SetEntityAsModified(object)"/>, therefore setting it as an entity requiring an update within the underlying dbcontext
        /// </summary>
        /// <param name="entityToUpdate">the entity to update</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            AttachEntity(entityToUpdate);
            context.SetEntityAsModified(entityToUpdate);
        }
    }
}
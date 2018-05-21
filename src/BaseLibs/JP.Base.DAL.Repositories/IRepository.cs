namespace JP.Base.DAL.Repositories
{
    /// <summary>
    /// defines base actions to perform in a repository
    /// </summary>
    /// <typeparam name="TEntity">the entity that your repo should implement</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entityToDelete">the <see cref="TEntity"/> object you wish to delete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">the id of the entity that you wish to delete</param>
        void DeleteById(object id);

        /// <summary>
        /// returns an entity of type TEntity based on id
        /// </summary>
        /// <param name="id">the id used to identify the entity</param>
        /// <returns>the entity found or null</returns>
        TEntity GetById(object id);

        /// <summary>
        /// inserts the entity into the dbcontext and marks it as ready for an insert into the database
        /// </summary>
        /// <param name="entity">entity to be inserted</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Sets an entity ready for an update within the underlying dbcontext
        /// </summary>
        /// <param name="entityToUpdate">the entity to update</param>
        void Update(TEntity entityToUpdate);
    }
}
using System;

namespace JP.Base.DAL.EF6.Repositories.Exceptions
{
    /// <summary>
    /// Defines an exception that will only happen when the entity to be deleted is null
    /// <para><seealso cref="Implementation.GenericRepository{TEntity}.DeleteById(object)"/></para>
    /// </summary>
    public class EntityToDeleteIsNullException : Exception
    {
        public EntityToDeleteIsNullException(object entityId)
        {
        }

        public object EntityId { get; set; }
    }
}
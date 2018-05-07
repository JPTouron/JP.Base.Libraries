using JP.Base.DAL.EF6.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace JP.Base.DAL.Repositories.Implementation
{
    internal class DataReaderAdapter : DbDataAdapter
    {
        public int Fill_From_Reader(DataTable dataTable, IDataReader dataReader)
        {
            return this.Fill(dataTable, dataReader);
        }
    }

    /// <summary>
    /// provides an implementation of <see cref="IGenericRepositoryEf{TEntity}"/>
    /// <para>you are not required to base your implementation on this</para>
    /// </summary>
    /// <typeparam name="TEntity">the type of the entity to be managed by this repository</typeparam>
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : DataRow
    {
        DbConnection conn;

        public GenericRepository(DbConnection conn)
        {
            this.conn = conn;
        }

        //protected virtual IQueryable<TEntity> Table(string mainQuery) {

        //    DbDataReader Reader;
        //    var cmd = conn.CreateCommand();
        //    cmd.CommandText = mainQuery;
        //    cmd.CommandType = CommandType.Text;
        //    Reader =   cmd.ExecuteReader();
        //    var dt = Reader_To_Table(ref Reader, true);

        //    return dt;

        //}


        private DataTable Reader_To_Table(ref DbDataReader Reader, bool DestroyReader =true)
        {
            var dt = new DataTable();
            var adapter = new DataReaderAdapter();
            dt.Locale = CultureInfo.CurrentCulture;

            try
            {
                adapter.Fill_From_Reader(dt, Reader);

                if (DestroyReader)
                {
                    Reader.Close();
                    adapter.Dispose();
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }



        /// <summary>
        /// given an <see cref="TEntity"/> type, this method invokes <see cref="DbSet.Remove(object)"/>  method
        /// <para>if the entity is not attached to context then it attaches it through the <see cref="AttachEntity(TEntity)"/> method</para>
        /// </summary>
        /// <param name="entityToDelete">the <see cref="TEntity"/> object you wish to delete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
           
        }

        /// <summary>
        /// finds an entity through <see cref="DbSet.Find(object[])"/> and invokes <see cref="Delete(TEntity)"/> method to delete it
        /// </summary>
        /// <exception cref="EntityToDeleteIsNullException">thrown when entity is not found</exception>
        /// <param name="id">the id of the entity that you wish to delete</param>
        public virtual void DeleteById(object id)
        {
            //TEntity entityToDelete = dbSet.Find(id);

            //if (entityToDelete == null)
            //    throw new EntityToDeleteIsNullException(id);

            //Delete(entityToDelete);
        }


        /// <summary>
        /// provides a way to obtain a collection out of the dbSet by means of a query
        /// </summary>
        /// <param name="filter"> an expression that would equal the Where part of a query</param>
        /// <param name="orderBy">the order by function to apply to the dbset</param>
        /// <param name="skip">pagination tool which records to skip before we start querying the db</param>
        /// <param name="take">pagination tool equals TOP from Sql</param>
        /// <returns>the IQueryable{TEntity} with the results obtained from the query or null if no results were found</returns>
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            int skip = 0,  
            int take = 0) 
        {

            DbDataReader Reader;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "select  * from table";
            cmd.CommandType = CommandType.Text;
            Reader = cmd.ExecuteReader();

            var l = Select<TEntity>(Reader, filter);


            var dt = Reader_To_Table(ref Reader, true);

            return dt;


            var query = dt.AsEnumerable().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            query.Where(x => x.Field<int>("") == 1);



            if (orderBy != null)
                query = orderBy(query);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);






            return query;
        }


        public  IEnumerable<TEntity> Select<TEntity>(this IDataReader reader,
                                       Func<IDataReader, TEntity> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }

        /// <summary>
        /// returns an entity of type TEntity based on id by executing the <seealso cref="DbSet.Find(object)"/> method
        /// </summary>
        /// <param name="id">the id used to identify the entity</param>
        /// <returns>the entity found or null</returns>
        public virtual TEntity GetById(object id)
        {
            //return dbSet.Find(id);
            return null;
        }

        /// <summary>
        /// inserts the entity into the underlying dbset
        /// </summary>
        /// <param name="entity">entity to be inserted</param>
        public virtual void Insert(TEntity entity)
        {
            //dbSet.Add(entity);
        }

        /// <summary>
        /// invokes the <see cref="AttachEntity(TEntity)"/> method and the <see cref="IDbContext.SetEntityAsModified(object)"/>, therefore setting it as an entity requiring an update within the underlying dbcontext
        /// </summary>
        /// <param name="entityToUpdate">the entity to update</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            //AttachEntity(entityToUpdate);
            //context.SetEntityAsModified(entityToUpdate);
        }
    }
}
using JP.Base.DAL.EF6.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF6.POC
{
    class Repo : IGenericRepositoryEf<Model>
    {
        public DbSet<Model> dbSet
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AttachEntity(Model entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Model entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Model> Get(Expression<Func<Model, bool>> filter = null, Func<IQueryable<Model>, IOrderedQueryable<Model>> orderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Model> Get(string includeNavigationProperties, bool dontTrack, Expression<Func<Model, bool>> filter = null, Func<IQueryable<Model>, IOrderedQueryable<Model>> orderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public Model GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Model entity)
        {
            throw new NotImplementedException();
        }

        public bool IsEntityUnchanged(Model entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Model entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

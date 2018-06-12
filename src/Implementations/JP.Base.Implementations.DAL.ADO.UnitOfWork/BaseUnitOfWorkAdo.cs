using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Repositories;
using System;
using System.Collections.Generic;

namespace JP.Base.Implementations.DAL.ADO.UnitOfWork
{
    public class BaseUnitOfWorkAdo : IBaseUnitOfWorkAdo
    {
        protected IDbAdoConnection currentConnection;
        protected IDictionary<Type, object> repos;

        private IDbConnFactory factory;

        public BaseUnitOfWorkAdo(IDbConnFactory factory)
        {
            this.factory = factory;
            repos = new Dictionary<Type, object>();
        }

        public bool IsDisposed
        {
            get; private set;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Execute(Action meth, bool saveChanges = false)
        {
            using (currentConnection = GetConnection())
            {
                currentConnection.Open();
                currentConnection.BeginTransaction();

                meth.Invoke();

                if (saveChanges)
                    currentConnection.CommitTransaction();
                else
                    currentConnection.RollbackTansacton();
            }
        }

        public TResult Execute<TResult>(Func<TResult> meth)
        {
            using (currentConnection = GetConnection())
            {
                currentConnection.Open();

                return meth.Invoke();
            }
        }

        public TResult Execute<TIn, TResult>(Func<TIn, TResult> meth, TIn arg)
        {
            using (currentConnection = GetConnection())
            {
                currentConnection.Open();

                return meth.Invoke(arg);
            }
        }

        public IGenericRepository<TModel> GetGenericRepo<TModel>() where TModel : class
        {
            var repo = repos[typeof(TModel)] as IGenericRepository<TModel>;
            return repo;
        }

        public TRepo GetSpecificRepo<TRepo>() where TRepo : class
        {
            var repo = repos[typeof(TRepo)] as TRepo;
            return repo;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
            }
        }

        private IDbAdoConnection GetConnection()
        {
            if (currentConnection == null || currentConnection.IsDisposed)
                currentConnection = factory.GetConnection();

            return currentConnection;
        }
    }
}
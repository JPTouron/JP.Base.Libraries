using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Repositories;
using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using System;
using System.Collections.Generic;

namespace JP.Base.DAL.ADO.UnitOfWork.Implementations
{
    public class BaseUnitOfWorkAdo : IBaseUnitOfWorkAdo
    {
        protected IDbAdoConnection currentConnection;
        protected IDictionary<Type, object> repos;

        //private string connectionString = "";
        //private string dataProvider;
        private IDbConnFactory factory;

        public BaseUnitOfWorkAdo(IDbConnFactory factory)
        {
            //this.dataProvider = dataProvider;
            //this.connectionString = connectionString;
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

                meth.Invoke();
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
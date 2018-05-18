using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using System;

namespace JP.Base.DAL.ADO.UnitOfWork.Implementations
{
    internal class BaseUnitOfWorkAdo : IBaseUnitOfWorkAdo
    {
        private string connectionString = "";
        private IDbAdoConnection currentConnection;
        private string dataProvider;
        private IDbConnFactory factory;

        public BaseUnitOfWorkAdo(IDbConnFactory factory, string dataProvider = "", string connectionString = "")
        {
            this.dataProvider = dataProvider;
            this.connectionString = connectionString;
            this.factory = factory;
        }

        public IExecutionData ExecutionData
        {
            get
            {
                if (currentConnection != null && !currentConnection.IsDisposed)
                    return new ExecutionData(currentConnection);
                else
                    return null;
            }
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
            using (var conn = GetConnection())
            {
                conn.Open(true);

                meth.Invoke();
            }
        }

        public TResult Execute<TResult>(Func<TResult> meth)
        {
            using (var conn = GetConnection())
            {
                conn.Open(true);

                return meth.Invoke();
            }
        }

        public TResult Execute<TIn, TResult>(Func<TIn, TResult> meth, TIn arg)
        {
            using (var conn = GetConnection())
            {
                conn.Open(true);

                return meth.Invoke(arg);
            }
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
                currentConnection = factory.GetConnection(dataProvider, connectionString);

            return currentConnection;
        }
    }
}
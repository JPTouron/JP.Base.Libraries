using JP.Base.DAL.EF6.Contracts;
using JP.Base.DAL.EF6.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace JP.Base.DAL.EF6.UnitOfWork
{
    /// <summary>
    /// base class implementation to model a UnitOfWork
    /// </summary>
    public abstract class BaseUnitOfWorkEf : IBaseUnitOfWorkEf
    {
        /// <summary>
        /// the respositories dictionary from which all the possible repositories will be obtained
        /// </summary>
        protected IDictionary<Type, object> repos;

        private const string invalidException = "Cannot use the Unit of Work when it has been disposed";

        private IContextProvider ctxtFactory;

        public BaseUnitOfWorkEf(IContextProvider ctxtFactory)
        {
            IsDisposed = false;
            repos = new Dictionary<Type, object>();
            this.ctxtFactory = ctxtFactory;
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

        protected IDbContext Context
        {
            get
            {
                return ctxtFactory.ProvideContext();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// see <see cref="IBaseUnitOfWork.Execute(Action, bool)"/> for more information
        /// </summary>
        public void Execute(Action meth, bool saveChanges = false)
        {
            CheckIfDisposed();
            using (Context)
            {
                meth.Invoke();

                if (saveChanges)
                    Save();
            }
        }

        /// <summary>
        /// see <see cref="IBaseUnitOfWork.Execute{TResult}(Func{TResult})"/> for more information
        /// </summary>
        public TResult Execute<TResult>(Func<TResult> meth)
        {
            CheckIfDisposed();
            using (Context)
            {
                return meth.Invoke();
            }
        }

        /// <summary>
        /// see <see cref="IBaseUnitOfWork.Execute{TIn, TResult}(Func{TIn, TResult}, TIn)"/> for more information
        /// </summary>
        public TResult Execute<TIn, TResult>(Func<TIn, TResult> meth, TIn arg)
        {
            CheckIfDisposed();
            using (Context)
            {
                return meth.Invoke(arg);
            }
        }

        /// <summary>
        /// see <see cref="IBaseUnitOfWork.GetGenericRepo{TModel}"/> for more information
        /// </summary>
        public IGenericRepository<TModel> GetGenericRepo<TModel>() where TModel : class
        {
            CheckIfDisposed();
            return repos[typeof(TModel)] as IGenericRepository<TModel>;
        }

        public TRepo GetSpecificRepo<TRepo>() where TRepo : class
        {
            CheckIfDisposed();
            return repos[typeof(TRepo)] as TRepo;
        }

        /// <summary>
        /// verifies the <see cref="IsDisposed"/> property, and if true then it throws <see cref="InvalidOperationException"/>
        /// </summary>
        protected void CheckIfDisposed()
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException(invalidException);
            }
        }

        /// <summary>
        /// disposes of this class and the underlying context if it exist as well
        /// <para>
        /// override if you need extra or different validation
        /// </para>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                if (disposing && Context != null && !Context.IsDisposed)
                {
                    Context.Dispose();
                }
            }
        }

        /// <summary>
        /// actual method to call <see cref="IDbContext.SaveChanges"/>
        /// <para>override if you need to do anything else here</para>
        /// </summary>
        protected virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}
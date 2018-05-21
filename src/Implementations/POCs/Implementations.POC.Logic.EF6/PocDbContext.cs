using JP.Base.DAL.EF6.Contracts;
using System;
using System.Data.Entity;

namespace Implementations.POC.Logic.EF6
{
    public class PocDbContext : DbContext, IDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employer> Employers { get; set; }

        public bool IsDisposed
        {
            get; private set;
        }

        public bool IsEntityDetached(object entity)
        {
            return Entry(entity).State == EntityState.Detached;

        }

        public bool IsEntityUnchanged(object entity)
        {
            return Entry(entity).State == EntityState.Unchanged;
        }

        public void SetEntityAsModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                base.Dispose(disposing);

                GC.SuppressFinalize(this);
            }
        }
    }
}
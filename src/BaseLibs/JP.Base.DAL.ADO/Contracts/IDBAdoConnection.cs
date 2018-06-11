using System;

namespace JP.Base.DAL.ADO.Contracts
{
    public interface IDbAdoConnection : IDbConnectionExecutables, IDisposable
    {
        string ConnHash { get; }

        bool IsDisposed { get; }

        bool Open();
    }
}
using JP.Base.DAL.ADO.Commands;
using System;
using System.Collections.Generic;
using System.Data;

namespace JP.Base.DAL.ADO.Contracts
{
    public interface IDbAdoConnection : IDisposable
    {
        string ConnHash { get; }

        bool IsDisposed { get; }

        void AddCommandParameter(ParameterData paramData);

        void AddCommandParameter(IEnumerable<ParameterData> param);

        void BeginTransaction();

        void Close();

        void CommitTransaction();

        void CreateCommand(CommandData data);

        int ExecuteNonQueryCommand();

        DataTable ExecuteReaderCommand();

        object ExecuteScalarCommand();

        bool Open();

        void RollbackTansacton();
    }
}
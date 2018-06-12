using JP.Base.DAL.ADO.Commands;
using System.Collections.Generic;
using System.Data;

namespace JP.Base.DAL.ADO.Contracts
{
    /// <summary>
    /// provides actions to perform actions within an ado.net connection
    /// </summary>
    public interface IDbConnectionExecutables
    {
        void AddCommandParameter(ParameterData paramData);

        void AddCommandParameter(IEnumerable<ParameterData> param);

        void BeginTransaction();

        void Close();

        void CommitTransaction();

        void CreateCommand(CommandData data);

        void CreateCommand(string commandText);

        int ExecuteNonQueryCommand();

        DataTable ExecuteReaderCommand();

        T ExecuteScalarCommand<T>();

        void RollbackTansacton();
    }
}
using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Commands;
using JP.Base.DAL.ADO.UnitOfWork.Contracts;
using System.Collections.Generic;
using System.Data;

namespace JP.Base.DAL.ADO.UnitOfWork.Implementations
{
    internal class ExecutionData : IExecutionData
    {
        private IDbAdoConnection conn;

        public ExecutionData(IDbAdoConnection conn)
        {
            this.conn = conn;
        }

        public void AddCommandParameter(List<ParameterData> param)
        {
            conn.AddCommandParameter(param);
        }

        public void AddCommandParameter(ParameterData paramData)
        {
            conn.AddCommandParameter(paramData);
        }

        public void CreateCommand(string command, CommandType type = CommandType.Text, int timeout = 1000, List<ParameterData> param = null)
        {
            conn.CreateCommand(command, type, timeout, param);
        }

        public int ExecuteNonQueryCommand()
        {
            return conn.ExecuteNonQueryCommand();
        }

        public DataTable ExecuteReaderCommand()
        {
            return conn.ExecuteReaderCommand();
        }

        public object ExecuteScalarCommand()
        {
            return conn.ExecuteScalarCommand();
        }
    }
}
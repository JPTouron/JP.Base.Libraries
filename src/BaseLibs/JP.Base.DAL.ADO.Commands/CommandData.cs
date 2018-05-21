using System.Collections.Generic;
using System.Data;

namespace JP.Base.DAL.ADO.Commands
{
    public class CommandData
    {
        public string CommandText { get; set; }

        public int CommandTimeout { get; set; } = 1000;

        public CommandType CommandType { get; set; } = CommandType.Text;

        public IEnumerable<ParameterData> Params { get; set; }
    }
}
using System.Data;

namespace JP.Base.DAL.ADO.Commands
{
    public class ParameterData
    {
        public ParameterData()
        {
        }

        public ParameterData(string name, object value, ParameterDirection direction, DbType type, int size)
        {
            Name= name;
            Value = value;
            Direction = direction;
            Type = type;
            Size = size;
        }

        public ParameterData(string name, object value, ParameterDirection direction, DbType type) : this(name, value, direction, type, 0)
        {
        }

        public ParameterData(string name, object value, DbType type) : this(name, value, ParameterDirection.Input, type, 0)
        {
        }

        public ParameterDirection Direction { get; set; }

        public string Name{ get; set; }

        public int Size { get; set; }

        public DbType Type{ get; set; }

        public object Value { get; set; }

        
    }
}
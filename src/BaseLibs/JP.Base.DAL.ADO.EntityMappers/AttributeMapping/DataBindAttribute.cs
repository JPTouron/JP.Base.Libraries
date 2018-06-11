using System;
using System.Data;

namespace JP.Base.DAL.ADO.EntityMappers.AttributeMapping
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataBindAttribute : Attribute
    {
        public DataBindAttribute(string columnName, DbType type = DbType.String)
        {
            ColumnName = columnName;
            Type = type;
        }

        public string ColumnName
        {
            get; set;
        }

        public DbType Type { get; set; }
    }
}
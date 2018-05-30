using System;

namespace JP.Base.DAL.ADO.EntityMappers.AttributeMapping
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataBindAttribute : Attribute
    {
        public DataBindAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName
        {
            get; set;
        }
    }
}
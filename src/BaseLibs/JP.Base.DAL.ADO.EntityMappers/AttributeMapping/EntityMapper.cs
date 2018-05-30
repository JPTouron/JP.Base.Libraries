using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace JP.Base.DAL.ADO.EntityMappers.AttributeMapping
{
    public class EntityMapper
    {
        public IEnumerable<T> MapEntities<T>(DbDataReader dataReader) where T : class, new()
        {
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    T item = new T();
                    for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                    {
                        var objectProperty = GetTargetProperty<T>(dataReader.GetName(columnIndex));
                        if (objectProperty != null)
                        {
                            var dataValue = dataReader.GetValue(columnIndex);
                            if (objectProperty.PropertyType == typeof(List<int>))
                            {
                                objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i.Trim())).ToList<int>());
                            }
                            else
                            {
                                objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue);
                            }
                        }
                    }

                    yield return item;
                }
            }
            else
                yield return null;
        }

        public IEnumerable<T> MapEntities<T>(DataTable table) where T : class, new()
        {
            if (table != null && table.Rows.Count > 0)
            {
                var rows = table.Rows.Cast<DataRow>().ToList();

                foreach (var row in rows)
                {
                    var item = MapEntity<T>(row);
                    yield return item;
                }
            }
            else
                yield return null;
        }

        public T MapEntity<T>(DataRow dataRow) where T : class, new()
        {
            T item = new T();

            if (dataRow.Table != null)
            {
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    var objectProperty = GetTargetProperty<T>(column.ColumnName);
                    if (objectProperty != null)
                    {
                        var dataValue = dataRow[column.ColumnName];
                        objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue);
                    }
                }
            }

            return item;
        }

        private PropertyInfo GetTargetProperty<T>(string name)
        {
            return typeof(T).GetProperties()
                            .Where(p => p.GetCustomAttributes(typeof(DataBindAttribute), true)
                                .Where(a => ((DataBindAttribute)a).ColumnName == name)
                                .Any()
                                ).FirstOrDefault();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace JP.Base.DAL.ADO.EntityMappers.AttributeMapping
{
    public static class EntityMapper
    {
        public static IEnumerable<T> MapEntities<T>(this DbDataReader dataReader) where T : class, new()
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

        public static IEnumerable<T> MapEntities<T>(this DataTable table) where T : class, new()
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

        public static T MapEntity<T>(this DataTable table) where T : class, new()
        {
            if (table != null && table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                {
                    var item = MapEntity<T>(row);
                    return item;
                }
            }
            else
                return null;
        }

        public static T MapEntity<T>(this DataRow dataRow) where T : class, new()
        {
            T item = new T();

            if (dataRow.Table != null)
            {
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    var objectProperty = GetTargetProperty<T>(column.ColumnName);
                    if (objectProperty != null)
                    {
                        Type propertyType = objectProperty.PropertyType;

                        var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;

                        var dataValue = dataRow[column.ColumnName];

                        if (!DBNull.Value.Equals(dataValue))
                            dataValue = Convert.ChangeType(dataValue, targetType);

                        objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue);
                    }
                }
            }

            return item;
        }

        private static PropertyInfo GetTargetProperty<T>(string name)
        {
            //first try to get the prop with bindble attrib on it...

            var boundProperty = typeof(T).GetProperties()
                            .Where(p => p.GetCustomAttributes(typeof(DataBindAttribute), true)
                                .Where(a => ((DataBindAttribute)a).ColumnName == name)
                                .Any()
                                ).FirstOrDefault();

            //if unsuccessfull try to match by exact name
            if (boundProperty == null)
                boundProperty = typeof(T).GetProperties()
                                .FirstOrDefault(p => p.Name == name);

            return boundProperty;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}
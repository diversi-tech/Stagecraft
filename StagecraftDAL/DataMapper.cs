using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL
{
    public static class DataMapper
    {
        public static List<T> MapToList<T>(SqlDataReader dr) where T : new()
        {
            List<T> list = new List<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            while (dr.Read())
            {
                T obj = new T();
                foreach (var property in properties)
                {
                    if (HasColumn(dr, property.Name) && dr[property.Name] != DBNull.Value)
                    {
                        property.SetValue(obj, dr[property.Name]);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public static T MapToObj<T>(SqlDataReader dr) where T : new()
        {
            T obj = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (HasColumn(dr, property.Name) && dr[property.Name] != DBNull.Value)
                {
                    property.SetValue(obj, dr[property.Name]);
                }
            }
            return (T)obj;
        }
    private static bool HasColumn(SqlDataReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

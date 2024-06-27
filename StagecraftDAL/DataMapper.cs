﻿
using System.Data.SqlClient;
using System.Reflection;


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

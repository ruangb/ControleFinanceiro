using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using ControleFinanceiro.CrossCutting.Utilities;

namespace ControleFinanceiro.Data
{
    public static class DataSupport<T> where T : class
    {
        public static string GenerateSqlInsert(List<string> fieldNames)
        {
            return $@"INSERT INTO {typeof(T).Name} ({fieldNames.Parameterize().Replace("@", "")}) " +
                   $"VALUES ({fieldNames.Parameterize()})";
        }

        public static string GenerateSqlUpdate(int id, string sql)
        {
            return $@"UPDATE {typeof(T).Name} " +
                   $"SET {sql} " +
                   $"WHERE Id = {id}";
        }

        public static void SetCommandParametersForInsertByEntityValues(T entity, SqlCommand command, List<string> fieldNames)
        {
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (!prop.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute)) 
                    && prop.CustomAttributes.Any(x => x.AttributeType == typeof(ColumnAttribute)))
                {
                    command.Parameters.Add(prop.Name, GetSqlDbTypeByStruct(prop.PropertyType)).Value = prop.GetValue(entity, null);
                    fieldNames.Add(prop.Name);
                }
            }
        }

        public static string SetCommandParametersForUpdateByEntityValues(T entity, SqlCommand command)
        {
            string sql = string.Empty;
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (!prop.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute))
                    && prop.CustomAttributes.Any(x => x.AttributeType == typeof(ColumnAttribute)))
                {
                    sql += $"{prop.Name} = @{prop.Name},";
                    command.Parameters.Add(prop.Name, GetSqlDbTypeByStruct(prop.PropertyType)).Value = prop.GetValue(entity, null);
                }
            }

            return sql.Remove(sql.Length - 1);
        }

        public static SqlDbType GetSqlDbTypeByStruct(Type type)
        {
            SqlDbType sqlDbType = SqlDbType.VarChar;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                    sqlDbType = SqlDbType.DateTime;
                    break;
                case TypeCode.Int16:
                    sqlDbType = SqlDbType.SmallInt;
                    break;
                case TypeCode.Int32:
                    sqlDbType = SqlDbType.Int;
                    break;
                case TypeCode.Boolean:
                    sqlDbType = SqlDbType.Bit;
                    break;
                case TypeCode.Decimal:
                    sqlDbType = SqlDbType.Decimal;
                    break;
                default:
                    break;
            }

            return sqlDbType;
        }
    }
}
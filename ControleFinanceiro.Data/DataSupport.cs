using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace ControleFinanceiro.Data
{
    public static class DataSupport<T> where T : class
    {
        public static void GetEntityKeyValues(T entity, SqlCommand command, List<string> fieldNames)
        {
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (!prop.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute)))
                {
                    command.Parameters.Add(prop.Name, GetSqlDbTypeByStruct(prop.PropertyType)).Value = prop.GetValue(entity, null);
                    fieldNames.Add(prop.Name);
                }
            }
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
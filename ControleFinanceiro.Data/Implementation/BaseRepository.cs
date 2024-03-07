using ControleFinanceiro.CrossCutting.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ControleFinanceiro.Data.Implementation
{
    public class BaseRepository<T> where T : class 
    {
        public void ExecuteInsert(T entity, SqlCommand command)
        {
            List<string> fieldNames = [];

            GetEntityKeyValues(entity, command, fieldNames);

            command.CommandText = $"INSERT INTO {typeof(T).Name} ({fieldNames.Parameterize().Replace("@", "")}) " +
                                  $"VALUES ({fieldNames.Parameterize()})";

            command.ExecuteNonQuery();
        } 

        public void GetEntityKeyValues(T entity, SqlCommand command, List<string> fieldNames)
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

        private SqlDbType GetSqlDbTypeByStruct(Type type)
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

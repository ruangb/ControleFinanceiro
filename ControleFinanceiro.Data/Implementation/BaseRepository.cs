using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.CrossCutting.Utilities;
using static Dapper.SqlMapper;

namespace ControleFinanceiro.Data.Implementation
{
    public class BaseRepository<T> where T : class 
    {
       public Dictionary<string, object> GetEntityKeyValues(T entity)
        {
            Dictionary<string, object> entityKeyValues = new Dictionary<string, object>();

            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (!prop.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute)))
                {
                    entityKeyValues.Add(prop.Name, prop.GetValue(entity, null));
                }
            }

            return entityKeyValues;
        }

    }
}

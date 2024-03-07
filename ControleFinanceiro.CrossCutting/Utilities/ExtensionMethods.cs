using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.CrossCutting.Utilities
{
    public static class ExtensionMethods
    {
        public static string Parameterize(this List<string> listParameter)
        {
            string strParameter = string.Empty;
            listParameter.ForEach(x =>
            {
                strParameter += x + ",";
            });

            return strParameter.Remove(strParameter.Length - 1);
        }
    }
}

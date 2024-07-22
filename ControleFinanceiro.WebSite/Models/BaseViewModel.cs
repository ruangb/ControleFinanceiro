using System.ComponentModel;

namespace ControleFinanceiro.WebSite.Models
{
    public class BaseViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.WebSite.Models
{
    public class CreditCardViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        public required string Name { get; set; }

        [DisplayName("Dia de Vencimento")]
        public required short DueDay { get; set; }

        public required short ClosingDays { get; set; }

        public required bool Inactive { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.WebSite.Models
{
    public class CreditCardViewModel : BaseViewModel
    {
        [DisplayName("Nome")]
        public required string Name { get => name; set => name = value.Trim(); }

        [DisplayName("Dia de Vencimento")]
        public required short DueDay { get; set; }

        [DisplayName("Dias de fechamento antes do vencimento")]
        public required short ClosingDays { get; set; }

        [DisplayName("Inativo")]
        public required bool Inactive { get; set; }

        public required string name;

        #region Text Properties
        public static string? ObjectName { get => "Pessoa"; }
        public static string? Pronoun { get => "a"; }
        #endregion
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.WebSite.Models
{
    public class BillViewModel : BaseViewModel
    {
        [DisplayName("Status")]
        public required string Status { get; set; }

        [DisplayName("Data de Vencimento")]
        [DataType(DataType.Date)]
        public required DateTime DueDate { get; set; }

        [DisplayName("Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [DisplayName("Cartão de Crédito")]
        public int? IdCreditCard { get; set; }

        public CreditCardViewModel? CreditCard { get; set; }

        #region Text Properties
        public static string? ObjectName { get => "Fatura"; }
        public static string? ObjectNamePlural { get => "Faturas"; }
        public static string? Pronoun { get => "a"; }
        #endregion
    }
}

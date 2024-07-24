using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.WebSite.Models
{
    public class ExpenseViewModel : BaseViewModel
    {
        [DisplayName("Status")]
        public required string Status { get; set; }

        [DisplayName("Data Transação")]
        [DataType(DataType.Date)]
        public DateTime OperationDate { get; set; }

        [DisplayName("Descrição")]
        [MinLength(2, ErrorMessage = "Informe ao menos {1} caracteres")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {1} caracteres")]
        public required string Description { get; set; }

        [DisplayName("Valor")]
        [DeniedValues(0, ErrorMessage = "O valor não pode ser zero")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DisplayName("Parcelas")]
        [DeniedValues(0, ErrorMessage = "Deve ser informado no mínimo 1 parcela")]
        public short ParcelQuantity { get; set; }

        [DisplayName("Pessoa")]
        [DeniedValues(0, ErrorMessage = "Selecione um valor")]
        public required int IdPerson { get; set; }

        [DisplayName("Cartão")]
        public int? IdCreditCard { get; set; }

        public required PersonViewModel Person { get; set; }

        public CreditCardViewModel? CreditCard { get; set; }

        public List<ExpenseInstallmentViewModel> ExpenseIntallments { get; set; }

        #region Text Properties
        public static string? ObjectName { get => "Despesa"; }
        public static string? ObjectNamePlural { get => "Despesas"; }
        public static string? Pronoun { get => "a"; }
        #endregion
    }
}

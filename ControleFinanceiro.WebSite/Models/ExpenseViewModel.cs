using ControleFinanceiro.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.WebSite.Models
{
    public class ExpenseViewModel : BaseViewModel
    {
        public required string Status { get; set; }

        [DisplayName("Data da Transa��o")]
        public required DateTime OperationDate { get; set; }

        [DisplayName("Descri��o")]
        [MinLength(2, ErrorMessage = "Informe ao menos {1} caracteres")]
        [MaxLength(100, ErrorMessage = "Informe no m�ximo {1} caracteres")]
        public required string Description { get; set; }

        [DisplayName("Valor")]
        [DeniedValues(0, ErrorMessage = "O valor n�o pode ser zero")]
        public required decimal Amount { get; set; }

        [DisplayName("Quantidade de Parcelas")]
        [DeniedValues(0, ErrorMessage = "Deve ser informado no m�nimo 1 parcela")]
        public required short ParcelQuantity { get; set; }

        [DisplayName("Pessoa")]
        public int IdPerson { get; set; }

        [DisplayName("Cart�o de Cr�dito")]
        public int IdCreditCard { get; set; }

        public required Person Person { get; set; }

        public CreditCard? CreditCard { get; set; }

        #region Text Properties
        public static string? ObjectName { get => "Despesa"; }
        public static string? ObjectNamePlural { get => "Despesas"; }
        public static string? Pronoun { get => "a"; }
        #endregion
    }
}

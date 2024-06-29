using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.WebSite.Models
{
    public class ExpenseInstallmentViewModel
    {
        [DisplayName("Despesa")]
        public required int IdExpense { get; set; }

        [DisplayName("Fatura")]
        public int IdBill { get; set; }

        [DisplayName("Parcela")]
        public short Installment { get; set; }

        [DisplayName("Status")]
        public required string Status { get; set; }

        [DisplayName("Data da Transação")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [DisplayName("Valor")]
        public decimal Value { get; set; }
        public required ExpenseViewModel Expense { get; set; }
        public required BillViewModel Bill { get; set; }

        #region Text Properties
        public static string? ObjectName { get => "Parcela da Despesa"; }
        public static string? ObjectNamePlural { get => "Parcelas da Despesa"; }
        public static string? Pronoun { get => "a"; }
        #endregion
    }
}

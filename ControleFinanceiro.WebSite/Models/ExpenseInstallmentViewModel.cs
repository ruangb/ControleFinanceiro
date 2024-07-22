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

        [DisplayName("Data Referência")]
        [DataType(DataType.Date)]
        public DateTime ReferenceDate { get; set; }

        [DisplayName("InstallmentOf")]
        public string InstallmentOf { get => $"{Installment}/{Expense.ParcelQuantity}"; }

        [DisplayName("Valor")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("ExpenseInstallment")]
    public class ExpenseInstallment
    {
        [Key]
        [Column]
        [Required]
        [ForeignKey("IdExpense")]
        public int IdExpense { get; set; }
        [Column]
        [ForeignKey("Installment")]
        public short Installment { get; set; }
        [Column]
        public string Status { get; set; }
        [Column]
        public DateTime DueDate { get; set; }
        [Column]
        public decimal Value { get; set; }
        public Expense Expense { get; set; }

        public ExpenseInstallment(int idExpense, short installment, string status, DateTime dueDate, decimal value)
        {
            IdExpense = idExpense;
            Installment = installment;
            Status = status;
            DueDate = dueDate;
            Value = value;
        }

        public ExpenseInstallment()
        {
        }
    }
}

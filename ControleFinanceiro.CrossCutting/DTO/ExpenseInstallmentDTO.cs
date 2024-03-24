namespace ControleFinanceiro.CrossCutting.DTO
{
    public class ExpenseInstallmentDTO
    {
        public int IdExpense { get; set; }
        public short Installment { get; set; }
        public required string Status { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Value { get; set; }
        public required ExpenseDTO Expense { get; set; }
    }
}

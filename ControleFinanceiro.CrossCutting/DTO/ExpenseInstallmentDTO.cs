namespace ControleFinanceiro.CrossCutting.DTO
{
    public class ExpenseInstallmentDTO : BaseDTO
    {
        public int IdExpense { get; set; }
        public int IdBill { get; set; }
        public short Installment { get; set; }
        public required string Status { get; set; }
        public DateTime ReferenceDate { get; set; }
        public decimal Value { get; set; }
        public required ExpenseDTO Expense { get; set; }
        public required BillDTO Bill { get; set; }
    }
}

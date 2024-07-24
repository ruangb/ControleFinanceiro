namespace ControleFinanceiro.CrossCutting.DTO
{
    public class ExpenseDTO : BaseDTO
    {
        public required string Status { get; set; }
        public DateTime OperationDate { get; set; }
        public required string Description { get; set; }
        public decimal Amount { get; set; }
        public short ParcelQuantity { get; set; }
        public int IdPerson { get; set; }
        public int? IdCreditCard { get; set; }
        public required PersonDTO Person { get; set; }
        public CreditCardDTO? CreditCard { get; set; }
        public List<ExpenseInstallmentDTO>? ExpenseInstallments { get; set; }
    }
}

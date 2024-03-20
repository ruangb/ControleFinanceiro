namespace ControleFinanceiro.CrossCutting.DTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public required string Status { get; set; }
        public required DateTime OperationDate { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        public required short ParcelQuantity { get; set; }
        public int IdPerson { get; set; }
        public int IdCreditCard { get; set; }
        public required PersonDTO Person { get; set; }
        public CreditCardDTO? CreditCard { get; set; }
    }
}

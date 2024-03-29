﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Column]
        public required string Status { get; set; }
        [Column]
        public DateTime OperationDate { get; set; }
        [Column]
        public required string Description { get; set; }
        [Column]
        public decimal Amount { get; set; }
        [Column]
        public short ParcelQuantity { get; set; }
        [Column]
        [Required]
        [ForeignKey("IdPerson")]
        public int IdPerson { get; set; }
        [Column]
        [ForeignKey("IdCreditCard")]
        public int? IdCreditCard { get; set; }
        public required Person Person { get; set; }
        public CreditCard? CreditCard { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Column]
        public required string Name { get; set; }
        [Column]
        public required bool Main { get; set; }
        [Column]
        public required bool Inactive { get; set; }
    }
}

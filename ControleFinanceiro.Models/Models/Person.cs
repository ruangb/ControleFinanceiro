using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required bool Main { get; set; }
        public required bool Inactive { get; set; }
    }
}

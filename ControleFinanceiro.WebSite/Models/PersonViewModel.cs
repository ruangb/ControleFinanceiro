using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.WebSite.Models
{
    public class PersonViewModel : BaseViewModel
    {
        [DisplayName("Nome")]
        public required string Name { get => name; set => name = value.Trim(); }

        [DisplayName("Principal")]
        public required bool Main { get; set; }

        [DisplayName("Inativo")]
        public required bool Inactive { get; set; }

        public required string name;

        #region Text Properties
        public static string? ObjectName { get => "Pessoa"; }
        public static string? Pronoun { get => "a"; }
        #endregion
    }
}

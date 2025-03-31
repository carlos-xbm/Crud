using System.ComponentModel.DataAnnotations;
namespace Crud.Models
{
    public class ContatoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o sobrenome!")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Digite o Email!"),
        EmailAddress(ErrorMessage ="Email incorreto!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o cargo!")]
        public string Cargo { get; set; }
    }
}

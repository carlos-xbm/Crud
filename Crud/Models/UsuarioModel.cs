using Crud.Enums;
using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class UsuarioModel : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string LoginUsuario { get; set; }

        public string Email { get; set; }

        public string? Senha { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public PerfilEnum Perfil { get; set; }

        // Validação personalizada
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Id == 0 && string.IsNullOrWhiteSpace(Senha))
            {
                yield return new ValidationResult("A senha é obrigatória no cadastro.", new[] { nameof(Senha) });
            }
        }
    }


}

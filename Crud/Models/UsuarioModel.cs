using Crud.Enums;
using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string LoginUsuario { get; set; }

        public string Email { get; set; }
                
        public string Senha { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public PerfilEnum Perfil { get; set; }

    }
}

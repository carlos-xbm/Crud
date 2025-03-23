using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Insumo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Insumo!")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Digite o nome do Fornecedor!")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage ="Digite o numero da Nota Fiscal!")]
        public string NF { get; set; }

        [Required(ErrorMessage ="Digite a Quantidade!")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage ="Digite a data de Fabricação!")]
        public DateTime Fabricacao { get; set; }

        public DateTime Vencimento { get; set; }

        [Required(ErrorMessage ="Digite a data de Entrada!")]
        public DateTime Entrada { get; set; }

        [Required(ErrorMessage ="Digite o Lote")]
        public string Lote { get; set; }
    }
}

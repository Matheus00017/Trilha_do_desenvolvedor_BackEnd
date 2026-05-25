using System.ComponentModel.DataAnnotations;

namespace ControleDeEstoque.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
    }
}
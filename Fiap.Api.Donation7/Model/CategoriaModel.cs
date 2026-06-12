using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation7.Model
{
    public class CategoriaModel
    {
        public int CategoriaId { get; set; }

        [Required]
        public string? NomeCategoria { get; set; }

        [Required]
        public string? Descricao { get; set; }

    }
}

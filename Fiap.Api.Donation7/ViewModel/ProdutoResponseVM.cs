namespace Fiap.Api.Donation7.ViewModel
{
    public class ProdutoResponseVM
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Disponivel { get; set; }
        public string? Descricao { get; set; }
        public string SugestaoTroca { get; set; }
        public double Valor { get; set; }
        public DateTime DataExpiracao { get; set; }
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }
        public string NomeUsuario { get; set; }
    }
}

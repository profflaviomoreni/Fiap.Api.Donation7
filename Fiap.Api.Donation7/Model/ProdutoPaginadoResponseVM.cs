using Fiap.Api.Donation7.ViewModel;

namespace Fiap.Api.Donation7.Model
{
    public class ProdutoPaginadoResponseVM
    {
        public int TotalGeral { get; set; }

        public int TotalPaginas { get; set; }

        public string LinkAnterior { get; set; }

        public string LinkProximo { get; set; } 

        public IList<ProdutoResponseVM> Produtos { get; set; }

    }
}

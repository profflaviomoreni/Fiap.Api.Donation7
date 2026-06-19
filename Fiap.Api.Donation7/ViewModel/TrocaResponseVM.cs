using Fiap.Api.Donation7.Model;

namespace Fiap.Api.Donation7.ViewModel
{
    public class TrocaResponseVM
    {

        public Guid TrocaId { get; set; } = Guid.NewGuid();

        public TrocaStatus TrocaStatus { get; set; }

        public int ProdutoIdEscolhido { get; set; }

        public string ProdutoNomeEscolhido { get; set; }

        public int ProdutoIdMeu { get; set; }

        public string ProdutoNomeMeu { get; set; }
    }
}

using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;

namespace Fiap.Api.Donation7.Services
{
    public class TrocaService : ITrocaService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ITrocaRepository _trocaRepository;

        public TrocaService(IProdutoRepository produtoRepository, ITrocaRepository trocaRepository)
        {
            _produtoRepository = produtoRepository;
            _trocaRepository = trocaRepository;
        }

        public async Task Trocar(TrocaModel trocaModel)
        {
            var produtoMeu = await _produtoRepository.FindByIdAsync(trocaModel.ProdutoIdMeu);
            var produtoEscolhido = await _produtoRepository.FindByIdAsync(trocaModel.ProdutoIdEscolhido);


            if (produtoEscolhido == null)
            {
                throw new Exception("Produto escolhido não está mais disponível");
            }

            if (!produtoEscolhido.Disponivel)
            {
                throw new Exception("Produto escolhido não está mais disponível");
            }

            if (produtoMeu == null)
            {
                throw new Exception("O seu produto não está mais disponível");
            }

            if (!produtoMeu.Disponivel)
            {
                throw new Exception("O seu produto não está mais disponível");
            }

            if (produtoMeu.UsuarioId != trocaModel.UsuarioId)
            {
                throw new Exception("Possível fraude, vc escolheu um produto que não pertence a você");
            }

            if ((produtoMeu.Valor / produtoEscolhido.Valor) < 0.9)
            {
                throw new Exception("Os valores dos produtos não podem passar de 10% de diferença");
            }

            produtoMeu.Disponivel = false;
            await _produtoRepository.UpdateAsync(produtoMeu);

            produtoEscolhido.Disponivel = false;
            await _produtoRepository.UpdateAsync(produtoEscolhido);


            trocaModel.TrocaStatus = TrocaStatus.Iniciado;
            await _trocaRepository.Insert(trocaModel);
        }
    }
}

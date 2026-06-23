using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Fiap.Api.Donation7.Services;
using Moq;

namespace Fiap.Api.Donation7Test
{
    public class TrocaServiceTest
    {

        private readonly ITrocaService _trocaService;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly Mock<ITrocaRepository> _trocaRepository;


        public TrocaServiceTest()
        {
            _trocaRepository = new Mock<ITrocaRepository>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _trocaService = new TrocaService(_produtoRepository.Object, _trocaRepository.Object);
        }


        // Trocar_Deve_Falhar_ProdutoEscolhido_Indisponivel
        [Fact]
        public async Task Trocar_Deve_Falhar_ProdutoEscolhido_Indisponivel()
        {
            // Arrange
            var troca = new TrocaModel { ProdutoIdEscolhido = 1, ProdutoIdMeu = 2, UsuarioId = 100 };

            var produtoEscolhido = new ProdutoModel
            {
                ProdutoId = 1,
                UsuarioId = 100,
                Usuario = new UsuarioModel { UsuarioId = 100 },
                Disponivel = false,
                Valor = 100
            };

            var produtoMeu = new ProdutoModel
            {
                ProdutoId = 2,
                UsuarioId = 100,
                Usuario = new UsuarioModel { UsuarioId = 100 },
                Disponivel = true,
                Valor = 100
            };

            // Act
            _produtoRepository.Setup( r=> r.FindByIdAsync(1)).ReturnsAsync(produtoEscolhido);
            _produtoRepository.Setup(r => r.FindByIdAsync(2)).ReturnsAsync(produtoMeu);


            // Assert
            await Assert.ThrowsAsync<Exception>(() => _trocaService.Trocar(troca));
        }



        [Fact]
        public async Task Trocar_Deve_Falhar_ProdutoEscolhido_DoMesmoUsuario()
        {
            // Arrange
            var troca = new TrocaModel { ProdutoIdEscolhido = 1, ProdutoIdMeu = 2, UsuarioId = 100 };

            // Act
            // Act
            var produtoEscolhido = new ProdutoModel
            {
                ProdutoId = 1,
                UsuarioId = 100,
                Usuario = new UsuarioModel { UsuarioId = 100 },
                Disponivel = true,
                Valor = 100
            };

            var produtoMeu = new ProdutoModel
            {
                ProdutoId = 2,
                UsuarioId = 101,
                Usuario = new UsuarioModel { UsuarioId = 101 },
                Disponivel = true,
                Valor = 100
            };

            _produtoRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(produtoEscolhido);
            _produtoRepository.Setup(r => r.FindByIdAsync(2)).ReturnsAsync(produtoMeu);


            // Assert
            await Assert.ThrowsAsync<Exception>(() => _trocaService.Trocar(troca));

        }


    }

}

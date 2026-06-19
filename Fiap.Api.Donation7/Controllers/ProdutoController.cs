using Asp.Versioning;
using AutoMapper;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Fiap.Api.Donation7.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    public class ProdutoController : BaseController
    {

        private readonly IProdutoRepository _produtoRepository;

        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<IList<ProdutoResponseVM>>> GetAll()
        {
            var produtos = await _produtoRepository.FindAllAsync();
            var produtosResponse = _mapper.Map<IList<ProdutoResponseVM>>(produtos);

            return Ok(produtosResponse);
        }


        [HttpGet]
        [ApiVersion("2.0")]
        public async Task<ActionResult<ProdutoPaginadoResponseVM>> GetAll([FromQuery]int pagina = 0, [FromQuery] int tamanho = 5)
        {

            var totalGeral = await _produtoRepository.Count();
            var produtosPaginado = new ProdutoPaginadoResponseVM();

            if (totalGeral > 0)
            {
                var produtos = await _produtoRepository.FindAllAsync(pagina, tamanho) ?? new List<ProdutoModel>();
                var totalPaginas = Convert.ToInt16( Math.Ceiling((double) totalGeral / tamanho) );

                produtosPaginado.TotalPaginas = totalPaginas;
                produtosPaginado.TotalGeral = totalGeral;
                produtosPaginado.LinkAnterior = (pagina > 0) ? $"/api/v2/produto?pagina={pagina - 1}&tamanho={tamanho}" : String.Empty;
                produtosPaginado.LinkProximo = (pagina < totalPaginas - 1) ? $"/api/v2/produto?pagina={pagina + 1}&tamanho={tamanho}" : String.Empty;

                produtosPaginado.Produtos = _mapper.Map<IList<ProdutoResponseVM>>(produtos);

            }

            return Ok(produtosPaginado);
            
        }



        [HttpGet]
        [ApiVersion("3.0")]
        public async Task<ActionResult<dynamic>> GetAllByRef([FromQuery] int idRef = 0, [FromQuery] int tamanho = 5)
        {

            var produtos = await _produtoRepository.FindAllByRefAsync(idRef, tamanho) ?? new List<ProdutoModel>();
            var ultimo = produtos.LastOrDefault();

            var retorno = new
            {
                Proximo = $"/api/v3/produto?idRef={ultimo.ProdutoId}&tamanho={tamanho}",
                Produtos = _mapper.Map<IList<ProdutoResponseVM>>(produtos)
            };

            return Ok(retorno);

        }

    }

}

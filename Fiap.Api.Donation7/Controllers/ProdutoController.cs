using AutoMapper;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Fiap.Api.Donation7.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly IProdutoRepository _produtoRepository;

        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IList<ProdutoResponseVM>>> GetAll()
        {
            var produtos = await _produtoRepository.FindAllAsync();
            var produtosResponse = _mapper.Map<IList<ProdutoResponseVM>>(produtos);

            return Ok(produtosResponse);
        }
        



    }
}

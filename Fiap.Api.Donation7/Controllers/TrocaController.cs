using Asp.Versioning;
using AutoMapper;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Fiap.Api.Donation7.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fiap.Api.Donation7.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("3.0")]
    public class TrocaController : BaseController
    {

        private readonly IMapper _mapper;

        private readonly IProdutoRepository _produtoRepository;

        private readonly ITrocaRepository _trocaRepository;


        public TrocaController(IMapper mapper, IProdutoRepository produtoRepository, ITrocaRepository trocaRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _trocaRepository = trocaRepository;
        }



        [HttpPost]
        public async Task<ActionResult<TrocaResponseVM>> Post([FromBody] TrocaRequestVM trocaRequestViewModel)
        {
            try
            {
                var trocaModel = _mapper.Map<TrocaModel>(trocaRequestViewModel);
                trocaModel.UsuarioId = GetUserId();

                #region Regra de negócio para efetuar a troca
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
                #endregion

                var trocaResponse = _mapper.Map<TrocaResponseVM>(trocaModel);
                return Ok(trocaResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }

}

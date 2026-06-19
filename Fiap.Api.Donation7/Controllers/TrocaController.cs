using Asp.Versioning;
using AutoMapper;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Fiap.Api.Donation7.Services;
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

        private readonly Services.ITrocaService _trocaService;


        public TrocaController(IMapper mapper, ITrocaService trocaService)
        {
            _mapper = mapper;
            _trocaService = trocaService;
        }



        [HttpPost]
        public async Task<ActionResult<TrocaResponseVM>> Post([FromBody] TrocaRequestVM trocaRequestViewModel)
        {
            try
            {
                var trocaModel = _mapper.Map<TrocaModel>(trocaRequestViewModel);
                trocaModel.UsuarioId = GetUserId();

                await _trocaService.Trocar(trocaModel);

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

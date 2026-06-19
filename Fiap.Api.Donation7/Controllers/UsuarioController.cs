using AutoMapper;
using Fiap.Api.Donation7.Data;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository;
using Fiap.Api.Donation7.Repository.Interfaces;
using Fiap.Api.Donation7.Services;
using Fiap.Api.Donation7.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {

        private readonly IUsuarioRepository _usuarioRepository;

        private readonly AuthTokenService _authTokenService;

        private readonly IMapper _mapper;


        public UsuarioController(IUsuarioRepository usuarioRepository, AuthTokenService authTokenService, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _authTokenService = authTokenService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IList<UsuarioResponseVM>>> GetAll()
        {
            var usuarios = await _usuarioRepository.FindAll();
            var listaUsuariosVM = _mapper.Map<IList<UsuarioResponseVM>>(usuarios);
            return Ok(listaUsuariosVM);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseVM>> GetById(int id)
        {
            var usuarioModel = await _usuarioRepository.FindById(id);
            if (usuarioModel == null)
                return NotFound();

            var usuarioVM = _mapper.Map<UsuarioResponseVM>(usuarioModel); 

            return Ok(usuarioVM);
        }


        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Post([FromBody] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioId = await _usuarioRepository.Insert(usuarioModel);
            usuarioModel.UsuarioId = usuarioId;

            return CreatedAtAction(nameof(GetById), new { id = usuarioId }, usuarioModel);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.UsuarioId)
                return BadRequest("ID da URL diferente do corpo da requisição.");

            await _usuarioRepository.Update(usuarioModel);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioRepository.FindById(id);
            if (usuario == null)
                return NotFound();

            await _usuarioRepository.Delete(id);
            return NoContent();
        }



        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginRequestVM loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _usuarioRepository.FindByEmailAndSenha(loginRequest.EmailUsuario, loginRequest.Senha);
            if (usuario == null)
                return Unauthorized();

            var loginResponse = _mapper.Map<LoginResponseVM>(usuario);
            loginResponse.Token = _authTokenService.GenerateToken(usuario.EmailUsuario, usuario.UsuarioId, usuario.Regra);

            return Ok(loginResponse);
        }

    }
}

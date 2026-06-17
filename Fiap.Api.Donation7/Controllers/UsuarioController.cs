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
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;

        private readonly AuthTokenService _authTokenService;

        private readonly IMapper _mapper;


        public UsuarioController(DataContext dataContext, IConfiguration configuration, IMapper mapper)
        {
            _usuarioRepository = new UsuarioRepository(dataContext);
            _authTokenService = new AuthTokenService(configuration);
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IList<UsuarioResponseVM>> GetAll()
        {
            var usuarios = _usuarioRepository.FindAll();
            var listaUsuariosVM = _mapper.Map<IList<UsuarioResponseVM>>(usuarios);
            return Ok(listaUsuariosVM);


            //var listaUsuariosVM = new List<UsuarioResponseVM>();

            //foreach (var item in usuarios)
            //{
            //    var usuarioVM = new UsuarioResponseVM();
            //    usuarioVM.UsuarioId = item.UsuarioId;
            //    usuarioVM.NomeUsuario = item.NomeUsuario;
            //    usuarioVM.EmailUsuario = item.EmailUsuario;

            //    listaUsuariosVM.Add(usuarioVM);
            //}
        }


        [HttpGet("{id}")]
        public ActionResult<UsuarioResponseVM> GetById(int id)
        {
            var usuarioModel = _usuarioRepository.FindById(id);
            if (usuarioModel == null)
                return NotFound();

            var usuarioVM = _mapper.Map<UsuarioResponseVM>(usuarioModel); 

            return Ok(usuarioVM);
        }


        [HttpPost]
        public ActionResult<UsuarioModel> Post([FromBody] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioId = _usuarioRepository.Insert(usuarioModel);
            usuarioModel.UsuarioId = usuarioId;

            return CreatedAtAction(nameof(GetById), new { id = usuarioId }, usuarioModel);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.UsuarioId)
                return BadRequest("ID da URL diferente do corpo da requisição.");

            _usuarioRepository.Update(usuarioModel);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.FindById(id);
            if (usuario == null)
                return NotFound();

            _usuarioRepository.Delete(id);
            return NoContent();
        }



        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResponseVM> Login([FromBody] LoginRequestVM loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = _usuarioRepository.FindByEmailAndSenha(loginRequest.EmailUsuario, loginRequest.Senha);
            if (usuario == null)
                return Unauthorized();

            var loginResponse = _mapper.Map<LoginResponseVM>(usuario);
            loginResponse.Token = _authTokenService.GenerateToken(usuario.EmailUsuario, usuario.UsuarioId, usuario.Regra);

            return Ok(loginResponse);
        }

    }
}

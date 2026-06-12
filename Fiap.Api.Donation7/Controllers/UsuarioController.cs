using Fiap.Api.Donation7.Data;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository;
using Fiap.Api.Donation7.Repository.Interfaces;
using Fiap.Api.Donation7.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(DataContext dataContext, IConfiguration configuration)
        {
            _usuarioRepository = new UsuarioRepository(dataContext);
        }


        [HttpGet]
        public ActionResult<IList<UsuarioResponseVM>> GetAll()
        {
            var usuarios = _usuarioRepository.FindAll();

            var listaUsuariosVM = new List<UsuarioResponseVM>();

            foreach (var item in usuarios)
            {
                var usuarioVM = new UsuarioResponseVM();
                usuarioVM.UsuarioId = item.UsuarioId;
                usuarioVM.NomeUsuario = item.NomeUsuario;
                usuarioVM.EmailUsuario = item.EmailUsuario;

                listaUsuariosVM.Add(usuarioVM);
            }


            return Ok(listaUsuariosVM);
        }


        [HttpGet("{id}")]
        public ActionResult<UsuarioResponseVM> GetById(int id)
        {
            var usuarioModel = _usuarioRepository.FindById(id);
            if (usuarioModel == null)
                return NotFound();


            var usuarioVM = new UsuarioResponseVM();
            usuarioVM.UsuarioId = usuarioModel.UsuarioId;
            usuarioVM.NomeUsuario = usuarioModel.NomeUsuario;
            usuarioVM.EmailUsuario = usuarioModel.EmailUsuario;


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
        public ActionResult<LoginResponseVM> Login(LoginRequestVM loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {

                var usuario = _usuarioRepository.FindByEmailAndSenha(loginRequest.EmailUsuario, loginRequest.Senha);

                var loginResponse = new LoginResponseVM
                {
                    NomeUsuario = usuario.NomeUsuario,
                    Token = "13132132.654564654654.313131313"
                };

                return Ok(loginResponse);
            }

        }

    }
}

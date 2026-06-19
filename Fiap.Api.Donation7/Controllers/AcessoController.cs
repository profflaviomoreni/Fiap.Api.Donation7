using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AcessoController : BaseController
    {

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonimo()
        {
            return "Anonimo";
        }


        [HttpGet]
        [Route("autenticado")]
        public string Autenticado()
        {
            int idUsuario = GetUserId();

            return "Autenticado";
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Admin()
        {
            return "Admin";
        }

        [HttpGet]
        [Route("operador")]
        [Authorize(Roles = "admin,operador")]
        public string Operador()
        {
            return "Operador";
        }


        [HttpGet]
        [Route("revisor")]
        [Authorize(Roles = "admin,operador,revisor")]
        public string Revisor()
        {
            return "Revisor";
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : BaseController
    {

        [HttpGet]
        public string Get()
        {
            return "Consultar";
        }
        
        [HttpPost]
        public string Cadastrar()
        {
            return "Cadastrar";
        }

        [HttpPut]
        public string Alterar()
        {
            return "Alterar";
        }
        
        [HttpDelete]
        public string Remover()
        {
            return "Remover";
        }


    }
}

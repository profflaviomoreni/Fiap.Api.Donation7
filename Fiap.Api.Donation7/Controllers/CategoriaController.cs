using Fiap.Api.Donation7.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        public IList<CategoriaModel> GetAll()
        {
            return new List<CategoriaModel>()
            {
                new CategoriaModel()
                {
                    CategoriaId = 1,
                    NomeCategoria = "Celular"
                },
                new CategoriaModel() {
                    CategoriaId = 2,
                    NomeCategoria = "Televisor"
                }
            };
        }

        [HttpGet("{id:int}")]
        public CategoriaModel GetById([FromRoute] int id)
        {
            return new CategoriaModel()
            {
                CategoriaId = 1,
                NomeCategoria = "Celular"
            };
        }

        [HttpPost]
        public int Post([FromBody] CategoriaModel categoriaModel)
        {
            return 10;
        }

        [HttpPut("{id:int}")]
        public void Put([FromRoute] int id, [FromBody] CategoriaModel categoriaModel)
        {
            Console.Write("Alterando");
        }

        [HttpDelete("{id:int}")]
        public void Delete([FromRoute] int id)
        {
            Console.Write("Deletando");
        }
    }
}

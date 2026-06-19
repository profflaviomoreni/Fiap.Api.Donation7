using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : BaseController
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IList<CategoriaModel>>> GetAll()
        {
            var categorias = await _categoriaRepository.FindAll() ?? new List<CategoriaModel>();
            return Ok(categorias);
        }

        

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaModel>> GetById([FromRoute] int id)
        {
            var categoria = await _categoriaRepository.FindById(id);

            if (categoria != null)
            {
                return Ok(categoria);
            } else
            {
                return NotFound();
            }
        }



        [HttpPost]
        public async Task<ActionResult<CategoriaModel>> Post([FromBody] CategoriaModel categoriaModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } else
            {
                categoriaModel.CategoriaId = await _categoriaRepository.Insert(categoriaModel);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = categoriaModel.CategoriaId },
                    categoriaModel
                );
            }

        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] CategoriaModel categoriaModel)
        {
            
            if(!ModelState.IsValid){ 
                return BadRequest(ModelState);
            }

            if ( id != categoriaModel.CategoriaId)
            {
                return BadRequest( new { error = "O id da URL deve ser igual ao id do corpo da requisição" });
            }

            if ( await _categoriaRepository.FindById(id) == null)
            {
                return NotFound();
            }

            await _categoriaRepository.Update(categoriaModel);

            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if ( id == 0)
            {
                return BadRequest();
            }

            if (await _categoriaRepository.FindById(id) == null)
            {
                return NotFound();
            }

            await _categoriaRepository.Delete(id);

            return NoContent();
        }
    }
}

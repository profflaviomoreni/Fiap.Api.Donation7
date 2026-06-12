using Fiap.Api.Donation7.Data;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository;
using Fiap.Api.Donation7.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(DataContext dataContext)
        {
            _categoriaRepository = new CategoriaRepository(dataContext);
        }


        [HttpGet]
        public ActionResult<IList<CategoriaModel>> GetAll()
        {
            var categorias = _categoriaRepository.FindAll() ?? new List<CategoriaModel>();
            return Ok(categorias);
        }

        

        [HttpGet("{id:int}")]
        public ActionResult<CategoriaModel> GetById([FromRoute] int id)
        {
            var categoria = _categoriaRepository.FindById(id);

            if (categoria != null)
            {
                return Ok(categoria);
            } else
            {
                return NotFound();
            }
        }



        [HttpPost]
        public ActionResult<CategoriaModel> Post([FromBody] CategoriaModel categoriaModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } else
            {
                categoriaModel.CategoriaId = _categoriaRepository.Insert(categoriaModel);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = categoriaModel.CategoriaId },
                    categoriaModel
                );
            }

        }



        [HttpPut("{id:int}")]
        public ActionResult Put([FromRoute] int id, [FromBody] CategoriaModel categoriaModel)
        {
            
            if(!ModelState.IsValid){ 
                return BadRequest(ModelState);
            }

            if ( id != categoriaModel.CategoriaId)
            {
                return BadRequest( new { error = "O id da URL deve ser igual ao id do corpo da requisição" });
            }

            if (_categoriaRepository.FindById(id) == null)
            {
                return NotFound();
            }

            _categoriaRepository.Update(categoriaModel);

            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if ( id == 0)
            {
                return BadRequest();
            }

            if (_categoriaRepository.FindById(id) == null)
            {
                return NotFound();
            }

            _categoriaRepository.Delete(id);

            return NoContent();
        }
    }
}

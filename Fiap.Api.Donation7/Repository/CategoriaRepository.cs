using Fiap.Api.Donation7.Data;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation7.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly DataContext _dataContext;

        public CategoriaRepository(DataContext dataContext) { 
            _dataContext = dataContext;
        }


        public async Task<IList<CategoriaModel>> FindAll()
        {
            return await _dataContext.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<CategoriaModel> FindById(int id)
        {
            return await _dataContext.Categorias
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task<int> Insert(CategoriaModel categoriaModel)
        {
            await _dataContext.Categorias.AddAsync(categoriaModel);
            await _dataContext.SaveChangesAsync();

            return categoriaModel.CategoriaId;
        }

        public async Task Update(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Update(categoriaModel);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var categoria = new CategoriaModel() { CategoriaId = id };

            _dataContext.Categorias.Remove(categoria);
            await _dataContext.SaveChangesAsync();
        }


    }
}

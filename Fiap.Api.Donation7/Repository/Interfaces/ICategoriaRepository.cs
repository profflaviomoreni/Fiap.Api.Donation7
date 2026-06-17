using Fiap.Api.Donation7.Model;

namespace Fiap.Api.Donation7.Repository.Interfaces
{
    public interface ICategoriaRepository
    {

        public Task<IList<CategoriaModel>> FindAll();
        public Task<CategoriaModel> FindById(int id);
        public Task<int> Insert(CategoriaModel categoriaModel);
        public Task Update(CategoriaModel categoriaModel);
        public Task Delete(int id);


    }
}

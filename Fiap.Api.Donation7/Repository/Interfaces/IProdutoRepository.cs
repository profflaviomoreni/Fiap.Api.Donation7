using Fiap.Api.Donation7.Model;

namespace Fiap.Api.Donation7.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        public Task<IList<ProdutoModel>> FindAllAsync();
        public Task<ProdutoModel> FindByIdAsync(int id);
        public Task<int> InsertAsync(ProdutoModel produtoModel);
        public Task UpdateAsync(ProdutoModel produtoModel);
        public Task DeleteAsync(int id);
    }
}

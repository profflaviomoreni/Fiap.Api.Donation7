using Fiap.Api.Donation7.Data;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation7.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _dataContext;

        public ProdutoRepository(DataContext ctx)
        {
            _dataContext = ctx;
        }

        public async Task<IList<ProdutoModel>> FindAllAsync()
        {
            return await _dataContext.Produtos
                .AsNoTracking()
                    .Include(p => p.Categoria)
                    .Include(p => p.Usuario)
                    .ToListAsync();
        }


        public async Task<ProdutoModel> FindByIdAsync(int id)
        {
            return await _dataContext.Produtos
                .Include(c => c.Categoria)
                .Include(u => u.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProdutoId == id);
        }

        public async Task<int> InsertAsync(ProdutoModel produtoModel)
        {
            await _dataContext.Produtos.AddAsync(produtoModel);
            await _dataContext.SaveChangesAsync();
            return produtoModel.ProdutoId;
        }

        public async Task UpdateAsync(ProdutoModel produtoModel)
        {
            _dataContext.Produtos.Update(produtoModel);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProdutoModel produtoModel)
        {
            _dataContext.Produtos.Remove(produtoModel);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produtoModel = await FindByIdAsync(id);
            if (produtoModel != null)
            {
                await DeleteAsync(produtoModel);
            }
        }
    }
}

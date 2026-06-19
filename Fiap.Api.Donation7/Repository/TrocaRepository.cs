using Fiap.Api.Donation7.Data;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation7.Repository
{
    public class TrocaRepository : ITrocaRepository
    {

        private readonly DataContext dataContext;

        public TrocaRepository(DataContext context)
        {
            dataContext = context;
        }

        public async Task<Guid> Insert(TrocaModel trocaModel)
        {
            await dataContext.Trocas.AddAsync(trocaModel);
            await dataContext.SaveChangesAsync();

            return trocaModel.TrocaId;
        }


        public async Task<TrocaModel> FindById(Guid id)
        {
            var troca = await dataContext.Trocas.AsNoTracking()
                    .Include(t => t.ProdutoMeu)
                    .Include(t => t.ProdutoEscolhido)
                .FirstOrDefaultAsync(t => t.TrocaId == id);

            return troca;
        }

    }
}

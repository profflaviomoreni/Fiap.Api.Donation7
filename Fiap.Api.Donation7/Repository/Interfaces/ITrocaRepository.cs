using Fiap.Api.Donation7.Model;

namespace Fiap.Api.Donation7.Repository.Interfaces
{
    public interface ITrocaRepository
    {

        public Task<Guid> Insert(TrocaModel trocaModel);

        public Task<TrocaModel> FindById(Guid id);
    }
}

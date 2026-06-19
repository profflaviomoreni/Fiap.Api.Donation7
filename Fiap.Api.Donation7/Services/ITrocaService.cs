using Fiap.Api.Donation7.Model;

namespace Fiap.Api.Donation7.Services
{
    public interface ITrocaService
    {
        Task Trocar(TrocaModel trocaModel);
    }
}

using Fiap.Api.Donation7.Model;

namespace Fiap.Api.Donation7.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<IList<UsuarioModel>> FindAll();

        public Task<UsuarioModel> FindById(int id);

        public Task<UsuarioModel> FindByEmailAndSenha(string email, string senha);

        public Task<int> Insert(UsuarioModel usuarioModel);

        public Task Update(UsuarioModel usuarioModel);

        public Task Delete(int id);
    }
}

using Fiap.Api.Donation7.Model;

namespace Fiap.Api.Donation7.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        public IList<UsuarioModel> FindAll();

        public UsuarioModel FindById(int id);

        public UsuarioModel FindByEmailAndSenha(string email, string senha);

        public int Insert(UsuarioModel usuarioModel);

        public void Update(UsuarioModel usuarioModel);

        public void Delete(int id);
    }
}

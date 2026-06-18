using Fiap.Api.Donation7.Data;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation7.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DataContext _dataContext;

        public UsuarioRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<UsuarioModel>> FindAll()
        {
            return await _dataContext.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<UsuarioModel> FindById(int id)
        {
            var usuario = await _dataContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.UsuarioId == id);

            return usuario;
        }

        public async Task<UsuarioModel> FindByEmailAndSenha(string email, string senha)
        {
            var usuario = await _dataContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(
                    u => u.EmailUsuario == email &&
                         u.Senha == senha
                );

            return usuario;
        }

        public async Task Delete(int id)
        {
            var usuario = new UsuarioModel();
            usuario.UsuarioId = id;

            _dataContext.Usuarios.Remove(usuario);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<int> Insert(UsuarioModel usuarioModel)
        {
            _dataContext.Usuarios.Add(usuarioModel);
            await _dataContext.SaveChangesAsync();

            return usuarioModel.UsuarioId;
        }

        public async Task Update(UsuarioModel usuarioModel)
        {
            _dataContext.Usuarios.Update(usuarioModel);
            await _dataContext.SaveChangesAsync();
        }

    }
}

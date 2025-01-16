using ProManager.Domain.Entity;
using ProManager.Infrastructure.Data.Repositories;

namespace ProManager.Application.Services
{
    public class UsuarioService
    {
        private UsuarioRepository _repository;
        public UsuarioService()
        {
            _repository = new UsuarioRepository();
        }

        public async Task<Usuario?> FindAsync(string user)
        {
            return await _repository.FindAsync(user);
        }
    }
}

using ProManager.Domain.Entity;
using ProManager.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProManager.Infrastructure.Data.Repositories
{
    public class UsuarioRepository
    {
        public async Task<Usuario?> FindAsync(string user)
        { 
            return await Task.Run(() =>
                MockList()
                .Where(x => x.UserSystem.ToLower() == user.ToLower())
                .FirstOrDefault()
            ); 
        }
        private List<Usuario> MockList()
        {
            return new List<Usuario>
            {
                new Usuario { Id = 1, UserSystem = "master", SenhaSystem = "123456", TypeUserSystem = TypeUserSystem.Admin },
                new Usuario { Id = 2, UserSystem = "user01", SenhaSystem = "123456", TypeUserSystem = TypeUserSystem.Default }
            };
        }
    }
}

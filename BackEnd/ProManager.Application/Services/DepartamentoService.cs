using ProManager.Domain.Entity;
using ProManager.Infrastructure.Data.Repositories;

namespace ProManager.Application.Services
{
    public class DepartamentoService
    {
        public List<Departamento> GetALL()
        {
            return new DepartamentoRepository().GetAll(); 
        }
    }
}

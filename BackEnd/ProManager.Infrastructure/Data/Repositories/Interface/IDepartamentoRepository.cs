using ProManager.Domain.Entity;

namespace ProManager.Infrastructure.Data.Repositories.Interface
{
    public interface IDepartamentoRepository
    {
        List<Departamento> GetAll();
    }
}
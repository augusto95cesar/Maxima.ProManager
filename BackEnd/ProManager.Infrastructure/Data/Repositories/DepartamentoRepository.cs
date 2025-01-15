using ProManager.Domain.Entity;
using ProManager.Infrastructure.Data.Repositories.Interface;

namespace ProManager.Infrastructure.Data.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        public List<Departamento> GetAll()
        {
            return MockList();
        }

        private List<Departamento> MockList()
        {
            return new List<Departamento>
            {
                new Departamento { Id = "010", Nome = "BEBIDAS" },
                new Departamento { Id = "020", Nome = "CONGELADOS" },
                new Departamento { Id = "030", Nome = "LATICINIOS" },
                new Departamento { Id = "040", Nome = "VEGETAIS" }
            };
        }
    }
}

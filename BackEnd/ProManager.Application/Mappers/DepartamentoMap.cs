using ProManager.Application.DTOs.Output;
using ProManager.Domain.Entity;

namespace ProManager.Application.Mappers
{
    public static class DepartamentoMap
    {
        public static List<DepartamentoDTO> Map(this List<Departamento> l)
        {
            var r = new List<DepartamentoDTO>();

            foreach (var i in l)
            {
                r.Add(new DepartamentoDTO
                {
                    Código = i.Id,
                    Descrição = i.Nome
                });
            }

            return r;
        }
    }
}

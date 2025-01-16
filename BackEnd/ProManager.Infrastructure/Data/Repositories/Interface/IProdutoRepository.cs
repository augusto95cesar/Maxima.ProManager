using ProManager.Domain.Entity;

namespace ProManager.Infrastructure.Data.Repositories.Interface
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByCodigoAsync(string codigo); 
        Task AddAsync(Produto produto);
        Task<bool> UpdateAsync(Produto produto);
        Task<bool> DeleteAsync(string id);
    }
}

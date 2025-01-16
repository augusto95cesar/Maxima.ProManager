using ProManager.Domain.Entity;

namespace ProManager.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodosAsync(); 
        Task AdicionarAsync(Produto produto);
        Task<bool> AtualizarAsync(Produto produto);
        Task<bool> RemoverAsync(string id);
        Task<bool> ProdutoExist(string codigo);
    }
}

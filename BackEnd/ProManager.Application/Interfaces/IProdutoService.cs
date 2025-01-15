using ProManager.Domain.Entity;

namespace ProManager.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto> ObterPorIdAsync(int id);
        Task AdicionarAsync(Produto produto);
        Task<bool> AtualizarAsync(Produto produto);
        Task<bool> RemoverAsync(int id);
    }
}

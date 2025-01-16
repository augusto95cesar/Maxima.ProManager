using ProManager.Application.Interfaces;
using ProManager.Domain.Entity;
using ProManager.Infrastructure.Data.Repositories.Interface;

namespace ProManager.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync() => await _produtoRepository.GetAllAsync(); 
        public async Task AdicionarAsync(Produto produto) => await _produtoRepository.AddAsync(produto);

        public async Task<bool> AtualizarAsync(Produto produto) => await _produtoRepository.UpdateAsync(produto);

        public async Task<bool> RemoverAsync(string id) => await _produtoRepository.DeleteAsync(id);

        public async Task<bool> ProdutoExist(string codigo)
        {
            Produto? produto = await _produtoRepository.GetByCodigoAsync(codigo);

            return produto != null ? true : false;
        }
    }
}

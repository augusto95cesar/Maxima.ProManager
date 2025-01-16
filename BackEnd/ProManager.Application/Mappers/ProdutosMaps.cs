using ProManager.Application.DTOs.Input;
using ProManager.Application.DTOs.Output;
using ProManager.Domain.Entity;
using ProManager.Domain.Enum;

namespace ProManager.Application.Mappers
{
    public static class ProdutosMaps
    {
        public static async Task<IEnumerable<ProdutoDTO>> Map(this Task<IEnumerable<Produto>> produtosTask)
        {
            var produtos = await produtosTask;

            var r = new List<ProdutoDTO>();

            foreach (var p in produtos)
                r.Add(new ProdutoDTO
                {
                    Codigo = p.Codigo,
                    Departameto = p.Departamento,
                    Descricao = p.Descricao,
                    Preco = p.Preco
                });

            return r;
        }

        public static Produto Map(this ProdutoPostDTO p)
        {
            var r = new Produto
            {
                Id = Guid.NewGuid(),
                Codigo = p.Codigo,
                Descricao = p.Descricao,
                Preco = p.Preco,
                Departamento = p.Departamento,
                Status = true,
                Acoes = AcoesProduto.Editar
            };

            return r;
        }

    }
}

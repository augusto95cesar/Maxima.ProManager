using ProManager.Application.DTOs.Input;
using ProManager.Application.Interfaces;
using ProManager.Application.Services;
using ProManager.Domain.ValueObjects;

namespace ProManager.Application.EventHandlers
{
    public static class ProdutoRequestHandler
    {
        public static ValidationError ValidateProduto(this ProdutoPostDTO produto, IProdutoService _produtoService)
        {
            var departamentoService = new DepartamentoService();
            var result = new ValidationError();

            if (produto == null)
            {
                result.AddError("O produto não pode ser nulo.");
                return result;
            }
             
            if (_produtoService.ProdutoExist(produto.Codigo).Result)
            {
                result.AddError($"O produto '{produto.Codigo}' já está cadastrado.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(produto.Codigo.ToString()) || produto.Codigo.ToString().Length > 50)
                result.AddError("O campo 'Codigo' é obrigatório e deve ter no máximo 50 caracteres.");

            if (produto.Preco <= 0)
                result.AddError("O preço deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(produto.Descricao.ToString()))
                result.AddError("O campo 'Descricao' é obrigatório.");

            if (string.IsNullOrWhiteSpace(produto.Departamento.ToString()))
                result.AddError("O campo 'Departameto' é obrigatório");

            if (string.IsNullOrWhiteSpace(departamentoService.Get(produto.Departamento.ToString())?.Id))
                result.AddError("O campo 'Departameto' não está cadastrado!");

            return result;
        }
         
    }
}

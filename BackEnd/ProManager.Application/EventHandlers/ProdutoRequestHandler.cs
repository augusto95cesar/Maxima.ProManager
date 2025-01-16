using ProManager.Application.DTOs.Input; 
using ProManager.Domain.ValueObjects;

namespace ProManager.Application.EventHandlers
{
    public static class ProdutoRequestHandler
    {
        public static ValidationError ValidateProduto(this ProdutoPostDTO produto)
        {
            var result = new ValidationError();

            if (produto == null)
            {
                result.AddError("O produto não pode ser nulo.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(produto.Codigo.ToString()) || produto.Codigo.ToString().Length > 50)
                result.AddError("O campo 'Codigo' é obrigatório e deve ter no máximo 50 caracteres.");

            if (produto.Preco <= 0)
                result.AddError("O preço deve ser maior que zero.");

            return result;
        }
         
    }
}

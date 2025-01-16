using ProManager.Application.DTOs.Input;
using ProManager.Domain.ValueObjects; 

namespace ProManager.Application.EventHandlers
{
    public static  class UsuarioRequestHandler
    {
        public static ValidationError ValidateUsuario(this LoginDTO login)
        {
            var result = new ValidationError();

            if (login == null)
            {
                result.AddError("O usuario não pode ser nulo.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(login.User.ToString()) || login.User.ToString().Length > 10)
                result.AddError("O campo 'User' é obrigatório e deve ter no máximo 50 caracteres.");

            if (string.IsNullOrWhiteSpace(login.Password.ToString()) || login.Password.ToString().Length > 10)
                result.AddError("O campo 'Password' é obrigatório e deve ter no máximo 50 caracteres.");

            return result;
        }
    }
}

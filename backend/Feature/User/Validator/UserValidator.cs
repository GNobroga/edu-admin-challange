using EduAdmin.Feature.User.DTO;
using FluentValidation;

namespace EduAdmin.Feature.User.Validator;

public class UserValidator : AbstractValidator<UserRequestDTO>
{
    public UserValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail não é válido.");
    }
}
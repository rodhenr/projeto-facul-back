using FluentValidation;
using Site_Vendas_Fake_API.Models;

namespace Site_Vendas_Fake_API.Shared;

public class AuthenticateUserValidator : AbstractValidator<UsuarioLogin>
{
    public AuthenticateUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha inválida.");
    }
}

public class RegisterUserValidator : AbstractValidator<UsuarioCadastro>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Nome)            
            .NotEmpty().WithMessage("O campo {PropertyName} não pode estar vazio.")
            .Length(3, 20).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres.")
            .Must(username => !username.Any(char.IsWhiteSpace)).WithMessage("O campo {PropertyName} não pode conter espaços.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha inválida.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .MaximumLength(32).WithMessage("A senha não deve exceder 32 caracteres.")
            .Matches(@"[A-Z]+").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]+").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"[0-9]+").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\!\?\*\.]+").WithMessage("A senha deve conter pelo menos um dos seguintes caracteres: ! ? * .");

        RuleFor(x => x.ConfirmacaoSenha)
            .Equal(x => x.Senha).WithMessage("As senhas não correspondem.");
    }
}
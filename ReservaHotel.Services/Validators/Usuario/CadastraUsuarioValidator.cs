using FluentValidation;
using ReservaHotel.Domain.Model.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Extensions.Validators.Usuario
{
    public class CadastraUsuarioValidator:AbstractValidator<CadastroUsuarioDTO>
    {
        public CadastraUsuarioValidator()
        {
            RuleFor(a => a.Email).NotEmpty();
            RuleFor(a => a.Senha)
                .NotEmpty().WithMessage("A senha não pode estar vazia.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .MaximumLength(100).WithMessage("A senha deve ter no máximo 100 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
                .Matches(@"[!@#$%^&*(),.?\:{ }|<>]").WithMessage("A senha deve conter pelo menos um caractere especial.");
            RuleFor(a => a.Nome).NotEmpty();
            RuleFor(a => a.Telefone).MinimumLength(8).WithMessage("O Telefone deve conter ao menos 8 digitos");

        }
    }
}

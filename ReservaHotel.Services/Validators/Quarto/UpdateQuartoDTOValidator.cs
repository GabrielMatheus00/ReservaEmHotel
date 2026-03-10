using FluentValidation;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services.Validators.Quarto
{
    public class UpdateQuartoDTOValidator:AbstractValidator<UpdateQuartoDTO>
    {
        public UpdateQuartoDTOValidator()
        {
            RuleFor(q => q.Andar).GreaterThanOrEqualTo(0).WithMessage("É necessário informar o andar do quarto").When(q => q.Andar.HasValue);
            RuleFor(q => q.Numero).GreaterThanOrEqualTo(1).WithMessage("É necessário que o número do quarto seja no mínimo 1").When(q => q.Numero.HasValue);
            RuleFor(q => q.Tamanho).GreaterThanOrEqualTo(10).WithMessage("O tamanho mínimo de um quarto de hotel é 10m²")
            .When(q => q.Tamanho.HasValue);

            RuleFor(q => q.DiariaDolar).GreaterThan(0).When(q => q.DiariaDolar.HasValue);

            RuleFor(q => q.Id).NotEmpty();
            RuleFor(q => q.TipoQuarto).Custom((tipoQuarto, context) =>
            {
                if (!Enum.IsDefined(typeof(TipoQuarto), tipoQuarto))
                {
                    context.AddFailure("O Tipo do quarto deve ser válido");
                }
            }).When(q => q.TipoQuarto.HasValue);

        }
    }
}

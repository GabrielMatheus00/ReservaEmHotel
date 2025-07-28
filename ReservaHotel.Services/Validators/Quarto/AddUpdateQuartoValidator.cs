using FluentValidation;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Domain.Model.Enums;


namespace ReservaHotel.Extensions.Validators.Quarto
{
    public class AddUpdateQuartoValidator:AbstractValidator<AddUpdateQuartoDTO>
    {
        public AddUpdateQuartoValidator(bool editando = false)
        {
            RuleFor(q => q.Andar).GreaterThanOrEqualTo(0).WithMessage("É necessário informar o andar do quarto").When(q=> q.Andar.HasValue);
            RuleFor(q => q.Numero).GreaterThanOrEqualTo(1).WithMessage("É necessário que o número do quarto seja no mínimo 1").When(q => q.Numero.HasValue);
            RuleFor(q => q.Tamanho).GreaterThanOrEqualTo(10).WithMessage("O tamanho mínimo de um quarto de hotel é 10m²")
            .When(q => q.Tamanho.HasValue);

            RuleFor(q => q.DiariaDolar).GreaterThan(0).When(q => q.DiariaDolar.HasValue);

            RuleFor(q => q.Id).NotEmpty().When(q=> editando);
            RuleFor(q => q.TipoQuarto).Custom((tipoQuarto, context) =>
            {
                if (!Enum.IsDefined(typeof(TipoQuarto), tipoQuarto))
                {
                    context.AddFailure("O Tipo do quarto deve ser válido");
                }
            }).When(q=> q.TipoQuarto.HasValue);

            if (!editando)
            {
                RuleFor(q => q.TipoQuarto).NotNull();
                RuleFor(q => q.Ocupacao).NotEmpty();
                RuleFor(q => q.DiariaDolar).NotNull();
                RuleFor(q => q.Tamanho).NotNull();
                RuleFor(q => q.HotelId).NotNull();
                RuleFor(q => q.Numero).NotNull();
                RuleFor(q => q.Andar).NotNull();
            }
        }
    }
}

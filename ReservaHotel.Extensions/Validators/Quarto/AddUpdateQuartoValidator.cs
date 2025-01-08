using FluentValidation;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Domain.Model.Enums;


namespace ReservaHotel.Extensions.Validators.Quarto
{
    public class AddUpdateQuartoValidator:AbstractValidator<AddUpdateQuartoDTO>
    {
        public AddUpdateQuartoValidator()
        {
            RuleFor(q => q.Andar).GreaterThanOrEqualTo(0).WithMessage("É necessário informar o andar do quarto");
            RuleFor(q => q.Tamanho).NotNull().GreaterThanOrEqualTo(10).WithMessage("O tamanho mínimo de um quarto de hotel é 10m²");
            RuleFor(q => q.TipoQuarto).NotNull();
            RuleFor(q => q.Ocupacao).NotNull().NotEmpty();
            RuleFor(q => q.ValorDolar).NotNull().GreaterThan(0);
            RuleFor(q => q.HotelId).NotNull();
            RuleFor(q => q.TipoQuarto).Custom((tipoQuarto, context) =>
            {
                if (!Enum.IsDefined(typeof(TipoQuarto), tipoQuarto))
                {
                    context.AddFailure("O Tipo do quarto deve ser válido");
                }
            });
        }
    }
}

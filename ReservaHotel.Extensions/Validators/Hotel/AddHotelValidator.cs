using FluentValidation;
using ReservaHotel.Domain.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Extensions.Validators.Hotel
{
    public class AddHotelValidator : AbstractValidator<AddUpdateHotelDTO>
    {
        public AddHotelValidator()
        {
            RuleFor(hotel => hotel.Estrelas).NotNull().GreaterThanOrEqualTo(1).WithMessage("O Hotel deve possuir ao menos uma estrela").LessThanOrEqualTo(5).WithMessage("O Hotel deve possuir no máximo 5 estrelas");
            RuleFor(hotel => hotel.Nome).NotNull().NotEmpty().WithMessage("É necessário informar o nome do hotel").MinimumLength(10).MaximumLength(150);
            RuleFor(hotel => hotel.Andares).NotNull().GreaterThanOrEqualTo(0).WithMessage("Número de andares do Hotel é inválido");

        }
    }
}

using FluentValidation;
using ReservaHotel.Domain.Model.DTOs.Hotel;
using ReservaHotel.Extensions.Validators.Comuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Extensions.Validators.Hotel
{
    public class AddHotelValidator : AbstractValidator<AddHotelDTO>
    {
        public AddHotelValidator()
        {
            RuleFor(hotel => hotel.Estrelas).NumeroDeEstrelas();
            RuleFor(hotel => hotel.Nome).NomeObrigatorio();
            RuleFor(hotel => hotel.Andares).NumeroDeAndares();

        }
    }
}

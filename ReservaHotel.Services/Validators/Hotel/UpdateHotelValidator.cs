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
    public class UpdateHotelValidator:AbstractValidator<UpdateHotelDTO>
    {
        public UpdateHotelValidator()
        {
            RuleFor(a => a.Nome.ToString()).NomeObrigatorio().When(a=> !string.IsNullOrEmpty(a.Nome));
            RuleFor(a => a.Estrelas.Value).NumeroDeEstrelas().When(a => a.Estrelas.HasValue);
            RuleFor(a => a.Andares.Value).NumeroDeAndares().When(a => a.Andares.HasValue);
            RuleFor(a => a.Id).NotEmpty().WithMessage("É necessário informar o id do Hotel");
        }
    }
}

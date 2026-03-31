

using Mapster;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Model.DTOs.Hotel;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Domain.Model.Enums;
using System.Reflection.Metadata.Ecma335;


namespace ReservaHotel.Domain.Mapping
{
    public class HotelMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddHotelDTO, Hotel>()
                .Map(dest => dest.Ativo, src => true)
                .TwoWays();

            config.NewConfig<UpdateHotelDTO, Hotel>()
                .Ignore(dest => dest.Quartos)
                .IgnoreNullValues(true);

            config.NewConfig<AddQuartoDTO, Quarto>()
                .TwoWays();

            config.NewConfig<UpdateQuartoDTO, Quarto>()
                .IgnoreNullValues(true);
        }
    }
}

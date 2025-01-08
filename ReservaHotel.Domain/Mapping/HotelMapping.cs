
using AutoMapper;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Model.DTOs;
using ReservaHotel.Domain.Model.DTOs.Quarto;


namespace ReservaHotel.Domain.Mapping
{
    public class HotelMapping : Profile
    {
        public HotelMapping()
        {
            
            CreateMap<AddUpdateHotelDTO, Hotel>().ForMember(q=> q.Ativo, opts => opts.MapFrom(a=> true)).ReverseMap();
            CreateMap<AddUpdateQuartoDTO, Quarto>().ReverseMap();

        }
    }
}

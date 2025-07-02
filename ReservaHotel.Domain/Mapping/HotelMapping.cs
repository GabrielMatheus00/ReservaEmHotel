
using AutoMapper;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Model.DTOs.Hotel;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using System.Reflection.Metadata.Ecma335;


namespace ReservaHotel.Domain.Mapping
{
    public class HotelMapping : Profile
    {
        public HotelMapping()
        {
            #region Hotel
            CreateMap<AddHotelDTO, Hotel>().ForMember(q=> q.Ativo, opts => opts.MapFrom(a=> true)).ReverseMap();
            CreateMap<UpdateHotelDTO, Hotel>()
                .ForMember(q => q.Quartos, opts => opts.Ignore())
                .ForMember(q => q.Estrelas, opts => opts.MapFrom((src, dest) => this.PreservaInteiro(src.Estrelas, dest.Estrelas)))
                .ForMember(q => q.Andares, opts => opts.MapFrom((src, dest) => this.PreservaInteiro(src.Andares, dest.Andares)))
                .ForMember(q => q.Nome, opts => opts.MapFrom((src, dest) => this.PreservaString(src.Nome, dest.Nome)));


            #endregion

            #region Quarto
            CreateMap<AddUpdateQuartoDTO, Quarto>().ReverseMap();
            #endregion

        }
        private int? PreservaInteiro(int? origem, int destino)
        {
            return origem.HasValue ? origem : destino;
        }
        private string PreservaString(string origem, string destino) => string.IsNullOrEmpty(origem) ? destino : origem;
    }
}

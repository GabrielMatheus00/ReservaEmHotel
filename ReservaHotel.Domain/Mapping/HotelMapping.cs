
using AutoMapper;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Model.DTOs.Hotel;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Domain.Model.Enums;
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
                .ForMember(q => q.Estrelas, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.Estrelas, dest.Estrelas)))
                .ForMember(q => q.Andares, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.Andares, dest.Andares)))
                .ForMember(q => q.Nome, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.Nome, dest.Nome)));


            #endregion

            #region Quarto
            CreateMap<AddQuartoDTO, Quarto>()
                .ForMember(q => q.Numero, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.Numero, dest.Numero)))
                .ForMember(q => q.Tamanho, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.Tamanho, dest.Tamanho)))
                .ForMember(q => q.Ocupacao, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.Ocupacao, dest.Ocupacao)))
                .ForMember(q => q.DiariaDolar, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.DiariaDolar, dest.DiariaDolar)))
                .ForMember(q => q.Andar, opts => opts.MapFrom((src, dest) => this.PreservaValor(src.Andar, dest.Andar)))
                .ForMember(q => q.TipoQuarto, opts => opts.MapFrom((src, dest) => (TipoQuarto)this.PreservaValor((int?)(src.TipoQuarto), (int)dest.TipoQuarto)))
                .ForMember(q => q.HotelId, opts => opts.MapFrom((src, dest) => src.HotelId == Guid.Empty ? dest.HotelId : src.HotelId ))
                .ReverseMap();
            #endregion

        }
        private int? PreservaValor(int? origem, int destino)
        {
            return origem.HasValue ? origem : destino;
        }
        private string PreservaValor(string origem, string destino) => string.IsNullOrEmpty(origem) ? destino : origem;

        private float? PreservaValor(float? origem, float destino) => origem.HasValue ? origem : destino;
        private decimal? PreservaValor(decimal? origem, decimal destino) => origem.HasValue ? origem: destino;
    }
}

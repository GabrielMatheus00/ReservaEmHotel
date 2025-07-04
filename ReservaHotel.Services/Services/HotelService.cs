using AutoMapper;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Hotel;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Services.Services.Interfaces;

namespace ReservaHotel.Services.Services
{
    public class HotelService : IHotelService
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseBase<Guid> AdicionaHotel(AddHotelDTO dto)
        {
            ResponseBase<Guid> response = new ResponseBase<Guid>();
            try
            {
                Hotel hotel = _mapper.Map<Hotel>(dto);
                hotel.Id = Guid.NewGuid();
                hotel.Ativo = true;
                _unitOfWork.HotelRepository.Adicionar(hotel);
                _unitOfWork.SalvarAlteracoes();
                response.Data = hotel.Id;
                response.AddSuccess("Hotel cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                response.Data = Guid.Empty;
                response.AddError(ex.Message);
            }
            return response;
        }
        /// <summary>
        /// Busca um hotel pelo seu id
        /// </summary>
        /// <param name="id">Id do hotel</param>
        /// <returns>O Hotel encontrado, ou null caso não encontre</returns>
        public ResponseBase<Hotel> BuscaHotel(Guid id)
        {
            ResponseBase<Hotel> response = new ResponseBase<Hotel>();
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o ID do Hotel");
                Hotel? hotel = _unitOfWork.HotelRepository.BuscarPorId(id);
                if (hotel == null)
                    throw new Exception("Não foi possível encontrar o Hotel");
                hotel.Quartos = _unitOfWork.QuartoRepository.BuscarTodos(q => q.HotelId == hotel.Id && q.Ativo).ToList();
                response.Data = hotel;
            }
            catch(Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
            }
            return response;
            
        }

        public ResponseBase<List<Hotel>> BuscaHoteis()
        {
            ResponseBase<List<Hotel>> response = new ResponseBase<List<Hotel>>();
            try
            {
                var hoteis = _unitOfWork.HotelRepository.BuscarTodos(h => h.Ativo);
                response.Data = hoteis.ToList();

            }
            catch(Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
            }
            return response;
        }
        public ResponseBase<string> EditaHotel(UpdateHotelDTO dto)
        {
            var response = new ResponseBase<string>();
            try
            {
                var hotel = _unitOfWork.HotelRepository.BuscarPorId(dto.Id);
                if (hotel == null)
                    throw new Exception("Hotel não encontrado");
                hotel = _mapper.Map(dto,hotel);
                _unitOfWork.HotelRepository.Atualizar(hotel);
                _unitOfWork.SalvarAlteracoes();

            }
            catch(Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                
            }
            return response;
        }

        public ResponseBase<Guid> RemoveHotel(Guid id)
        {
            ResponseBase<Guid> response = new ResponseBase<Guid>();
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o ID do Hotel");
                var removido = _unitOfWork.HotelRepository.Remover(id);
                _unitOfWork.SalvarAlteracoes();
                if (!removido)
                    throw new Exception();
                response.Data = id;
                response.AddSuccess("Hotel removido com sucesso");
            }
            catch (Exception ex)
            {
                response.Data = Guid.Empty;
                response.AddError(ex.Message);
            }
            return response;
        }


        

        

        public ResponseBase<List<Quarto>> BuscaQuartosPorHotel(Guid hotelId)
        {
            ResponseBase<List<Quarto>> response = new ResponseBase<List<Quarto>>();
            try
            {
                if (hotelId == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o id do hotel");
                List<Quarto> quartos = _unitOfWork.QuartoRepository.BuscarTodos(q => q.HotelId == hotelId).ToList();
                response.Data = quartos;

            }
            catch(Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
            }
            return response;
        }
    }
}

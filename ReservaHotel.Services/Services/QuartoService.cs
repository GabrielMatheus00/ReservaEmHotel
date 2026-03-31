using MapsterMapper;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Extensions.Exceptions;
using ReservaHotel.Extensions.Validators.Quarto;
using ReservaHotel.Services.Services.Interfaces;
using ReservaHotel.Services.Validators.Quarto;

namespace ReservaHotel.Services.Services
{
    public class QuartoService:IQuartoService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public QuartoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseBase<Guid>> CadastraQuarto(AddQuartoDTO dto)
        {
            ResponseBase<Guid> response = new ResponseBase<Guid>();
            try
            {
                var validacao = new AddQuartoValidator().Validate(dto);
                if (!validacao.IsValid)
                    throw new ValidacaoException(validacao.Errors);
                ValidaQuarto(dto.HotelId, dto.Andar, dto.Numero);
                Quarto quarto = _mapper.Map<Quarto>(dto);
                quarto.Ativo = true;
                _unitOfWork.QuartoRepository.Adicionar(quarto);
                await _unitOfWork.SalvarAlteracoes();
                response.AddSuccess("Quarto cadastrado com sucesso");
                response.Data = quarto.Id;
            }
            catch (ValidacaoException ex)
            {
                response.Data = Guid.Empty;
                response.AddErrosValidacao(ex.Erros);
            }
            catch (Exception ex)
            {
                response.Data = Guid.Empty;
                response.AddError(ex.Message);
            }
            return response;
        }
        public async Task<ResponseBase<Guid>> RemoveQuarto(Guid id)
        {
            var response = new ResponseBase<Guid>();
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o Id para remoção!");
                bool removido = _unitOfWork.QuartoRepository.Remover(id);
                if (!removido)
                    throw new Exception("Não foi possível encontrar o quarto para remoção!");
                await _unitOfWork.SalvarAlteracoes();
                response.AddSuccess("Quarto removido com sucesso!");
                response.Data = id;
            }
            catch (Exception ex)
            {
                response.Data = Guid.Empty;
                response.AddError(ex.Message);
            }
            return response;
        }

        public async Task<ResponseBase<Guid>> EditaQuarto(UpdateQuartoDTO dto)
        {
            var response = new ResponseBase<Guid>();
            try
            {
                 var validacao = new UpdateQuartoDTOValidator().Validate(dto);
                if (!validacao.IsValid)
                    throw new ValidacaoException(validacao.Errors);
                Quarto quarto = _unitOfWork.QuartoRepository.BuscarPorId(dto.Id);
                if (quarto == null)
                    throw new Exception("Quarto não encontrado");
                ValidaQuarto(quarto.HotelId, dto.Andar ?? quarto.Andar, dto.Numero ?? quarto.Numero, dto.Id);
                quarto = _mapper.Map(dto, quarto);
                _unitOfWork.QuartoRepository.Atualizar(quarto);
                await _unitOfWork.SalvarAlteracoes();
                response.AddSuccess("Quarto atualizado com sucesso");
                response.Data = dto.Id;
            }
            catch (ValidacaoException ex)
            {
                response.Data = Guid.Empty;
                response.AddErrosValidacao(ex.Erros);
            }
            catch (Exception ex)
            {
                response.Data = Guid.Empty;
                response.AddError(ex.Message);
            }
            return response;
        }

        public ResponseBase<Quarto> BuscaQuarto(Guid id)
        {
            ResponseBase<Quarto> response = new ResponseBase<Quarto>();
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o Id do quarto");
                var quarto = _unitOfWork.QuartoRepository.BuscarPorId(id);
                if (quarto == null)
                    throw new Exception("Quarto não encontrado");
                response.Data = quarto;

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
            }
            return response;
        }
        private void ValidaQuarto(Guid hotelId, int andar, int numero, Guid? quartoId = null)
        {
            Hotel hotel = _unitOfWork.HotelRepository.BuscaHotelComQuartos(hotelId);
            if (hotel is null)
                throw new ArgumentException("Não foi possível encontrar o hotel!");

            if (andar > hotel.Andares)
                throw new ArgumentException("Número de andar inválido para o hotel em questão!");

            if(hotel.Quartos.Any(q => q.Numero == numero && q.Andar == andar &&  q.Ativo && (!quartoId.HasValue || q.Id != quartoId.Value)))
                throw new ArgumentException("Já existe um quarto cadastrado com esse número e andar para o hotel em questão!");
                           
        }
        public ResponseBase<List<Quarto>> BuscaQuartosPorHotel(Guid hotelId)
        {
            ResponseBase<List<Quarto>> response = new ResponseBase<List<Quarto>>();
            try
            {
                if (hotelId == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o id do hotel");
                List<Quarto> quartos = _unitOfWork.QuartoRepository.BuscarTodos(q => q.HotelId == hotelId && q.Ativo).ToList();
                response.Data = quartos;

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
            }
            return response;
        }
    }
}

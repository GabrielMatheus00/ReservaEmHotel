using AutoMapper;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Extensions.Exceptions;
using ReservaHotel.Extensions.Validators.Quarto;
using ReservaHotel.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ResponseBase<Guid> CadastraQuarto(AddUpdateQuartoDTO dto)
        {
            ResponseBase<Guid> response = new ResponseBase<Guid>();
            try
            {
                var validacao = new AddUpdateQuartoValidator().Validate(dto);
                if (!validacao.IsValid)
                    throw new ValidacaoException(validacao.Errors);
                ValidaAddUpdateQuarto(dto);
                
                Quarto quarto = _mapper.Map<Quarto>(dto);
                quarto.Ativo = true;
                _unitOfWork.QuartoRepository.Adicionar(quarto);
                _unitOfWork.SalvarAlteracoes();
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
        public ResponseBase<Guid> RemoveQuarto(Guid id)
        {
            var response = new ResponseBase<Guid>();
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o Id para remoção!");
                bool removido = _unitOfWork.QuartoRepository.Remover(id);
                if (!removido)
                    throw new Exception("Não foi possível encontrar o quarto para remoção!");
                _unitOfWork.SalvarAlteracoes();
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

        public ResponseBase<Guid> EditaQuarto(AddUpdateQuartoDTO dto)
        {
            var response = new ResponseBase<Guid>();
            try
            {
                 var validacao = new AddUpdateQuartoValidator(editando:true).Validate(dto);
                if (!validacao.IsValid)
                    throw new ValidacaoException(validacao.Errors);
                Quarto? quarto = _unitOfWork.QuartoRepository.BuscarPorId(dto.Id);
                if (quarto == null)
                    throw new Exception("Quarto não encontrado");
                ValidaAddUpdateQuarto(dto, quarto);
                quarto = _mapper.Map(dto, quarto);
                _unitOfWork.QuartoRepository.Atualizar(quarto);
                _unitOfWork.SalvarAlteracoes();
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
        private void ValidaAddUpdateQuarto(AddUpdateQuartoDTO dto, Quarto quarto = null)
        {
            Guid hotelId = quarto is null ? dto.HotelId : quarto.HotelId;
            Hotel hotel = _unitOfWork.HotelRepository.BuscaHotelComQuartos(hotelId);
            ValidaQuarto(hotel, dto, quarto);
        }

        private void ValidaQuarto(Hotel hotel, AddUpdateQuartoDTO dto, Quarto quarto = null)
        {
            if (hotel is null)
                throw new ArgumentException("Não foi possível encontrar o hotel!");
            //verifica se vai alterar o andar ou o número do quarto para verificar se já há um quarto com essa configuração
            if(quarto is not null && (dto.Andar.HasValue || dto.Numero.HasValue))
            {
                int andarQuarto = dto.Andar.HasValue ? dto.Andar.Value : quarto.Andar;
                int numeroQuarto = dto.Numero.HasValue ? dto.Numero.Value : quarto.Numero;
                if (hotel.Quartos.Any(q =>  q.Andar == andarQuarto && numeroQuarto == q.Numero && q.Ativo && q.Id != dto.Id))
                    throw new ArgumentException("Já existe um quarto com esse número no andar em questão!");
            }
           
            if (dto.Andar> hotel.Andares)
                throw new ArgumentException("Número de andar inválido para o hotel em questão!");
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

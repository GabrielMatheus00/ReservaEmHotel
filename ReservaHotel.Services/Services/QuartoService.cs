using AutoMapper;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Quarto;
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
                QuartoValido(dto);
                Quarto quarto = _mapper.Map<Quarto>(dto);
                quarto.Ativo = true;
                _unitOfWork.QuartoRepository.Adicionar(quarto);
                _unitOfWork.SalvarAlteracoes();
                response.AddSuccess("Quarto cadastrado com sucesso");
                response.Data = quarto.Id;
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

        public ResponseBase<string> EditaQuarto(AddUpdateQuartoDTO dto)
        {
            var response = new ResponseBase<string>();
            try
            {
                if (dto.Id == Guid.Empty)
                    throw new ArgumentNullException("É necessário informar o id do quarto");
                Quarto? quarto = _unitOfWork.QuartoRepository.BuscarPorId(dto.Id);
                if (quarto == null)
                    throw new Exception("Quarto não encontrado");
                quarto = _mapper.Map(dto, quarto);
                _unitOfWork.QuartoRepository.Atualizar(quarto);
                _unitOfWork.SalvarAlteracoes();
                response.AddSuccess("Quarto atualizado com sucesso");
            }
            catch (Exception ex)
            {
                response.Data = null;
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
        private void QuartoValido(AddUpdateQuartoDTO quarto)
        {
            var hotel = _unitOfWork.HotelRepository.BuscaHotelComQuartos(quarto.HotelId);
            if (hotel is null)
                throw new ArgumentException("Não foi possível encontrar o hotel!");
            if (hotel.Quartos.Any(q => q.Andar == quarto.Andar && quarto.Numero == q.Numero && q.Ativo))
                throw new ArgumentException("Já um hotel com esse número no andar em questão!");
            if (quarto.Andar > hotel.Andares)
                throw new ArgumentException("Número de andar inválido para o hotel em questão!");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ReservaHotel.Data.DataAccessLayer;


namespace Apresentation.Controllers
{
    
    public class AgendamentoController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgendamentoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
    }
}

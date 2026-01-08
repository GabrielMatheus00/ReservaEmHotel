using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<ResponseBase<Guid>> CadastroUsuario(CadastroUsuarioDTO dto);
        public ResponseBase<string> Login(LoginDTO dto);
    }
}

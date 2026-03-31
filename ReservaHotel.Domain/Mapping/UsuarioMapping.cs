using Mapster;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Model.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Mapping
{
    public class UsuarioMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CadastroUsuarioDTO, Usuario>()
                .TwoWays();
        }
    }
}

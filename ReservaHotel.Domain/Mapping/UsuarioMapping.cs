using AutoMapper;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.Migrations;
using ReservaHotel.Domain.Model.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Mapping
{
    public class UsuarioMapping: Profile
    {
        public UsuarioMapping()
        {
            CreateMap<CadastroUsuarioDTO, Usuario>().ReverseMap();

        }
    }
}

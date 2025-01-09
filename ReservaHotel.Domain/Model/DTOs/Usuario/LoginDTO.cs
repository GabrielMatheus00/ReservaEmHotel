using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Model.DTOs.Usuario
{
    public class LoginDTO
    {

        /// <summary>
        /// Pode ser tanto o Username quanto o Email
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string Senha { get; set; }

    }
}

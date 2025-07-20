using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Extensions.Exceptions
{
    public class ValidacaoException : Exception
    {

        public List<string> Erros { get; private set; }
        public ValidacaoException()
        {
        }

        public ValidacaoException(string? message) : base(message)
        {
        }
        public ValidacaoException(List<ValidationFailure> erros)
        {
            Erros = new List<string>();
            foreach(var erro in erros)
            {
                Erros.Add(erro.ErrorMessage);
            }
        }
    }
}

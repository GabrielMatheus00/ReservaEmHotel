using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.ResponseMapping
{
    public class ResponseBase<T>
    {
        public bool Success { get
            {
                return !this.Messages.Any(a=> a.Type == TypeMessage.Error);
            }
            private set { }
         }
        public T? Data { get; set; }
        public List<ResponseMessage> Messages { get; set; } = new List<ResponseMessage>();

        private void AddMessage(string content, TypeMessage type)
        {
            this.Messages.Add(new ResponseMessage(content, type));
        }
        public void AddSuccess(string content)
        {
            this.AddMessage(content, TypeMessage.Success);
        }
        public void AddError(string content)
        {
            this.AddMessage(content, TypeMessage.Error);
        }
        public void AddErrosValidacao(List<string> erros)
        {
            if (erros == null || !erros.Any())
                return;
            foreach(string erro in erros)
            {
                this.AddError(erro);
            }
        }
    }
    
}

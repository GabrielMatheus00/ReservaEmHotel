using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReservaHotel.Data.ResponseMapping
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        [JsonIgnore]
        public TypeMessage Type { get; set; }

        public ResponseMessage(string message, TypeMessage type)
        {
            Message = message;
            Type = type;
        }

    }
    public enum TypeMessage
    {
        Success =0,
        Error =1
    }
}

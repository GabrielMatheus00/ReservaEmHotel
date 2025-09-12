
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ReservaHotel.Extensions.Extensions
{
    public static class LoggingExtensions
    {
        
        public static void LogFormatado(this ILogger logger, Exception ex, object? dto = null, LogLevel logLevel= LogLevel.Error)
        {
            string objetoSerializado = string.Empty;
            if(dto != null)
            {
                objetoSerializado = JsonConvert.SerializeObject(dto);
            }
            logger.Log(logLevel, ex, objetoSerializado, null);
            
        }
    }
}

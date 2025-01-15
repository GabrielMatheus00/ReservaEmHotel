using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.DataAccessLayer.Repositories.Classes;
using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using ReservaHotel.Extensions.Validators.Hotel;
using ReservaHotel.Services.Services;
using ReservaHotel.Services.Services.Interfaces;

namespace ReservaHotel.Extensions.Extensions
{
    public static class BuilderExtensions
    {
        public static void InjecaoDeDependencia(this IServiceCollection services)
        {
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IQuartoRepository, QuartoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            
            services.AddValidatorsFromAssemblyContaining<AddHotelValidator>();
        }
    }
}

using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.DataAccessLayer.Repositories.Classes;
using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using ReservaHotel.Data.Database;
using ReservaHotel.Domain.Configuration;
using ReservaHotel.Domain.Mapping;
using ReservaHotel.Domain.Model.DTOs.Hotel;
using ReservaHotel.Extensions.Extensions.Hangfire;
using ReservaHotel.Extensions.Validators.Hotel;
using ReservaHotel.Services.Services;
using ReservaHotel.Services.Services.Interfaces;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg =>
{
    var executingAssembly = Assembly.GetExecutingAssembly();
    cfg.AddMaps(new[]
    {
        typeof(HotelMapping)
    });
}
);
var connectionString = builder.Configuration.GetConnectionString("HotelDatabase");


builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<AppConfig>(builder.Configuration);

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IQuartoRepository, QuartoRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IQuartoService, QuartoService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IValidator<AddHotelDTO>, AddHotelValidator>();
builder.Services.AddScoped<IHangfireService, HangfireService>();
builder.Services.ConfigHangfireServer(connectionString);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.StartDashboard();
RecurringJob.AddOrUpdate<IHangfireService>("Atualiza valor dólar", x => x.AtualizaValorDolar(), "0 12 * * 1-5");
RecurringJob.AddOrUpdate<IHangfireService>("Atualiza diaria quarto", x => x.AtualizaPrecoQuartos(), "0 7-17/1 * * 1-5");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();

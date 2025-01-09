using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservaHotel.Apresentation.Configuration;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.DataAccessLayer.Repositories.Classes;
using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Mapping;
using ReservaHotel.Domain.Model.DTOs;
using ReservaHotel.Extensions.Validators.Hotel;
using ReservaHotel.Services.Services;
using ReservaHotel.Services.Services.Interfaces;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<HotelMapping>();
}
);
var connectionString = builder.Configuration.GetConnectionString("HotelDatabase");


builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<Configuracoes>(builder.Configuration);

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IQuartoRepository, QuartoRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddValidatorsFromAssemblyContaining<AddHotelValidator>();
builder.Services.AddScoped<IValidator<AddUpdateHotelDTO>, AddHotelValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();

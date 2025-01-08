using Microsoft.EntityFrameworkCore;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.Database.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.Database
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }
        DbSet<Hotel> Hoteis { get; set; }
        DbSet<Quarto> Quartos { get; set; }

        DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new QuartoConfigutarion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}

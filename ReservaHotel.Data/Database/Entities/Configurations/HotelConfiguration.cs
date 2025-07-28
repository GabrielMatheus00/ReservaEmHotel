using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.Database.Entities.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Nome).IsRequired();
            builder.Property(h => h.NumeroQuartos).HasDefaultValue(0);
            builder.Property(h => h.Ativo).IsRequired().HasDefaultValue(true);
            builder.Property(h => h.Andares).HasDefaultValue(0);
            builder.Property(h => h.DataCadastro).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.HasMany(h => h.Quartos).WithOne(q => q.Hotel).HasForeignKey(a => a.HotelId);
            
        }
    }
}

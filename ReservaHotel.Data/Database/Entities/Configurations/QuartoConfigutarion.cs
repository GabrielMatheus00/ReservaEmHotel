using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.Database.Entities.Configurations
{
    public class QuartoConfigutarion : IEntityTypeConfiguration<Quarto>
    {
        public void Configure(EntityTypeBuilder<Quarto> builder)
        {
            builder.ToTable("Quartos"); 


            builder.HasKey(q => q.Id);

            builder.Property(q => q.Numero)
                .IsRequired();


            builder.Property(q => q.Ocupacao)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(q => q.HotelId)
                .IsRequired();

            builder.Property(q => q.TipoQuarto)
                .IsRequired();

            builder.Property(q => q.Ativo)
                .IsRequired()
                .HasDefaultValue(true); 

            builder.Property(q => q.Andar)
                .IsRequired();

            builder.Property(q => q.Tamanho)
                .IsRequired()
                .HasColumnType("real");

            builder.Property(q => q.UltimaAtualizacaoPreco)
                .HasColumnType("datetime2(7)");

            builder.Property(q => q.DataCadastro).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(q => q.CotacaoId);

            builder.HasOne(q => q.Hotel).WithMany(h => h.Quartos).HasForeignKey(q => q.HotelId);
            builder.HasOne(q => q.Cotacacao).WithMany(c => c.Quartos).HasForeignKey(q => q.CotacaoId);
        }
    }
}

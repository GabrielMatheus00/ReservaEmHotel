using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.Database.Entities.Configurations
{
    public class CotacaoMoedaConfiguration : IEntityTypeConfiguration<CotacaoMoeda>
    {
        public void Configure(EntityTypeBuilder<CotacaoMoeda> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Ativo).HasDefaultValue(true);
            builder.Property(c => c.DataCadastro).HasDefaultValueSql("NOW()");
            builder.Property(c => c.DataCotacao).IsRequired();
            builder.Property(c => c.Moeda).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CotacaoCompra).IsRequired();

        }
    }
}

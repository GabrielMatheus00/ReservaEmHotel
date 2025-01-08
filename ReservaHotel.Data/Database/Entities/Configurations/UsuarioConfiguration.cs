using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReservaHotel.Data.Database.Entities.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Telefone).IsRequired().HasMaxLength(15);
            builder.Property(u => u.DataNascimento).IsRequired();
            builder.Property(u => u.DataCadastro).IsRequired();
            builder.Property(u => u.Ativo).IsRequired().HasDefaultValue(true);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(70);
            builder.Property(u => u.Senha).IsRequired().HasMaxLength(100);
        }
    }
}

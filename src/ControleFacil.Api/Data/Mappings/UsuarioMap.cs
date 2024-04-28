using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ControleFacil.Api.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario", "usuario")
            .HasKey(p => p.Id);

            builder.Property(p => p.Email)
            .IsRequired();

            builder.Property(p => p.Senha)
            .IsRequired();

            builder.Property(p => p.DataCadastro)
            .IsRequired();

            builder.Property(p => p.DataInativacao);

            
        }
    }
}

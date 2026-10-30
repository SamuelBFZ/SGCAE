using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sgcae.Domain.Entities;

namespace sgcae.Persistence.Configurations
{
    internal class PuntoAccesoConfig : IEntityTypeConfiguration<PuntoAcceso>
    {
        public void Configure(EntityTypeBuilder<PuntoAcceso> builder)
        {
            builder.ToTable("PuntosAcceso");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.HasIndex(p => p.Nombre)
                   .IsUnique();

            builder.Property(p => p.Habilitado)
                   .IsRequired();
        }
    }
}

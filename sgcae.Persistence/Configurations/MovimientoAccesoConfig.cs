using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sgcae.Domain.Entities;

namespace sgcae.Persistence.Configurations
{
    internal class MovimientoAccesoConfig : IEntityTypeConfiguration<MovimientoAcceso>
    {
        public void Configure(EntityTypeBuilder<MovimientoAcceso> builder)
        {
            builder.ToTable("MovimientosAcceso");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.CodigoCredencial)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(m => m.PuntoAccesoSolicitado)
                   .IsRequired();

            builder.Property(m => m.Tipo)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(m => m.Resultado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(m => m.Motivo)
                   .HasConversion<string>()
                   .HasMaxLength(40);

            builder.Property(m => m.OcurridoEn)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.HasOne<Credencial>()
                   .WithMany()
                   .HasForeignKey(m => m.CredencialId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Empleado>()
                   .WithMany()
                   .HasForeignKey(m => m.EmpleadoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<PuntoAcceso>()
                   .WithMany()
                   .HasForeignKey(m => m.PuntoAccesoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(m => m.OcurridoEn);
            builder.HasIndex(m => new { m.EmpleadoId, m.OcurridoEn });
        }
    }
}

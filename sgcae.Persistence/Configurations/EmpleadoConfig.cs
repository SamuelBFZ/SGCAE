using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sgcae.Domain.Entities;

namespace sgcae.Persistence.Configurations
{
    internal class EmpleadoConfig : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleados");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Identificacion)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasIndex(e => e.Identificacion)
                   .IsUnique();

            builder.Property(e => e.Nombres)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(e => e.Apellidos)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(e => e.Correo)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.Cargo)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(e => e.Area)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(e => e.EstadoLaboral)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();
        }
    }
}

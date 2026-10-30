using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sgcae.Domain.Entities;

namespace sgcae.Persistence.Configurations
{
    internal class CredencialConfig : IEntityTypeConfiguration<Credencial>
    {
        public void Configure(EntityTypeBuilder<Credencial> builder)
        {
            builder.ToTable("Credenciales");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
                   .HasMaxLength(40)
                   .IsRequired();

            builder.HasIndex(c => c.Codigo)
                   .IsUnique();

            builder.Property(c => c.EmpleadoId)
                   .IsRequired();

            builder.HasOne<Empleado>()
                   .WithMany()
                   .HasForeignKey(c => c.EmpleadoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Estado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();
        }
    }
}

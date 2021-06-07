using BomTratoDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BomTratoData.Mapping
{
    public class ProcessoMap : IEntityTypeConfiguration<Processo>
    {
        public void Configure(EntityTypeBuilder<Processo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasKey(c => c.ProcessNumber);
            builder.Property(c => c.Value)
                .HasColumnType("decimal(12,2)")
                .HasPrecision(2)
                .IsRequired();
            builder.Property(c => c.Aproved)
                .HasColumnType("bit")
                .HasDefaultValue(false);
            builder.Property(c => c.Status)
                .HasColumnType("bit")
                .HasDefaultValue(false);
            builder.Property(c => c.ComplainerName)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            builder.HasOne(c => c.Aprovador)
               .WithMany(c => c.Processos);
            builder.HasOne(c => c.Escritorio)
               .WithMany(c => c.Processos);
        }
    }
}

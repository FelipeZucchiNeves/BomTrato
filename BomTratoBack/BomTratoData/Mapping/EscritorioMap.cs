using BomTratoDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BomTratoData.Mapping
{
    public class EscritorioMap : IEntityTypeConfiguration<Escritorio>
    {
        public void Configure(EntityTypeBuilder<Escritorio> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Street)
                .HasColumnType("varchar(50)")
                .HasPrecision(2)
                .IsRequired();
            builder.Property(c => c.State)
                .HasColumnType("varchar(4)")
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(c => c.Number)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(c => c.District)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.Cep)
                .HasColumnType("int")
                .HasMaxLength(8)
                .IsRequired();
            builder.Property(c => c.City)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            builder.HasMany(c => c.Processos)
                .WithOne(c => c.Escritorio);
        }
    }
}

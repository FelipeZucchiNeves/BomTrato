using BomTratoDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BomTratoData.Mapping
{
    public class AprovadorMap : IEntityTypeConfiguration<Aprovador>
    {
        public void Configure(EntityTypeBuilder<Aprovador> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");
            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.LastName)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.BirthDate)
                .HasColumnType("date")
                .HasMaxLength(100)
                .IsRequired();
            builder.HasMany(c => c.Processos)
                .WithOne(c => c.Aprovador);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Infrastructure.Mappings
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("ItemPedido");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Price)
                .HasColumnName("PRECO")
                .IsRequired()
                .HasPrecision(18,2);

            builder.Property(p => p.CreatedAt)
                .HasColumnName("DATA_CRIACAO")
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("DATA_ATUALIZACAO")
                .IsRequired();
        }
    }
}

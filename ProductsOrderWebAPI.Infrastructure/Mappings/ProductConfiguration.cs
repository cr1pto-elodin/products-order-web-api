using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Infrastructure.Mappings
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("ItemPedido", tb => tb.HasTrigger("TRG_UpdateOrderTotal"));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.IdOrder)
                .HasColumnName("ID_PEDIDO")
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnName("PRECO")
                .IsRequired()
                .HasPrecision(18,2);

            builder.Property(p => p.Name)
                .HasColumnName("NOME")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.CreatedAt)
                .HasColumnName("DATA_CRIACAO")
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("DATA_ATUALIZACAO")
                .IsRequired();
        }
    }
}

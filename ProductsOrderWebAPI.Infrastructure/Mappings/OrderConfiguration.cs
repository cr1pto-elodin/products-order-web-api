using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Infrastructure.Mappings
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Pedido");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.TotalPrice)
                .HasColumnName("VALOR_TOTAL")
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(o => o.CreatedAt)
                .HasColumnName("DATA_CRIACAO")
                .IsRequired();

            builder.Property(o => o.UpdatedAt)
                .HasColumnName("DATA_ATUALIZACAO")
                .IsRequired();
            
            builder.HasMany(order => order.ProductsList)
               .WithOne(item => item.Order)
               .HasForeignKey(item => item.IdOrder)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

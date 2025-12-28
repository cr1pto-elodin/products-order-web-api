using Microsoft.EntityFrameworkCore;
using ProductsOrderWebAPI.Domain.Interfaces;
using ProductsOrderWebAPI.Infrastructure.Data;
using ProductsOrderWebAPI.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ProductsOrderWebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=compra.db")
            );

            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}

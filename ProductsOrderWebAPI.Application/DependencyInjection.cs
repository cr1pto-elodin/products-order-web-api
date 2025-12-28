using ProductsOrderWebAPI.Application.Interfaces;
using ProductsOrderWebAPI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsOrderWebAPI.Application
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Repositories;
using ProductApi.Repositories.Implementation;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Implementation;
using ProductApi.Services.Interfaces;

namespace ProductApi.IoC
{
    public static class DiRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductOptionService, ProductOptionService>();

            // Repos
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductOptionRepository, ProductOptionRepository>();
        }
    }
}
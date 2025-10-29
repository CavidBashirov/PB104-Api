using Microsoft.Extensions.DependencyInjection;
using Service.Services.Interfaces;
using Service.Services;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IBookService,BookService>();
            services.AddScoped<IAccountService,AccountService>();
            return services;
        }
    }
}

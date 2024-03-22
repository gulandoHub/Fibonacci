using System;
using System.Linq;
using Fibonacci.Api.Filters;
using Fibonacci.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fibonacci.Api
{
    public static class DISetup
    {
        public static IServiceCollection RegisterInjections(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { cfg.AllowNullCollections = true; }, 
                    AppDomain.CurrentDomain.GetAssemblies().Where(x =>
                        x.FullName != null &&
                        x.FullName.Contains(nameof(Fibonacci), StringComparison.InvariantCultureIgnoreCase)), ServiceLifetime.Scoped);
            
            services.AddScoped<ValidationFilter>();
            
            return services;
        }
        
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
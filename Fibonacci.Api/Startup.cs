using Fibonacci.Service;
using Fibonacci.Service.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Fibonacci.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fibonacci.Api", Version = "v1" });
            });
            services.AddOptions();
            services.Configure<ApplicationSettings>(opts => Configuration.GetSection("ApplicationSettings").Bind(opts));

            services.RegisterInjections();
            services.RegisterFibonacciService();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureCustomExceptionMiddleware();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fibonacci.Api v1"));
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
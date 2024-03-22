using Microsoft.Extensions.DependencyInjection;
using Fibonacci.Service.Interfaces;
using Fibonacci.Service.Services;

namespace Fibonacci.Service
{
    public static class DISetup
    {
        public static IServiceCollection RegisterFibonacciService(this IServiceCollection services)
        {
            return 
                services
                    .AddSingleton<IGetFibonacciNumbers, GetFibonacciNumbersService>()
                    .AddMemoryCache();
        }
    }
}
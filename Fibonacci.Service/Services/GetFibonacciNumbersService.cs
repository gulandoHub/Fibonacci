using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using Fibonacci.Service.ExtensionMethods;
using Fibonacci.Service.Interfaces;
using Fibonacci.Service.Model;
using Fibonacci.Service.Model.Error;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Fibonacci.Service.Services
{
    public class GetFibonacciNumbersService : IGetFibonacciNumbers
    {
        private readonly IMemoryCache _cache;
        private readonly CacheItemPolicy _cacheItemPolicy;
        private readonly ApplicationSettings _applicationSettings;

        public GetFibonacciNumbersService(IMemoryCache cache, IOptions<ApplicationSettings> options)
        {
            _cache = cache;
            _applicationSettings = options.Value;

            _cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_applicationSettings.CacheTime)
            };
        }

        public List<int> GetFibonacciNumbers(FibonacciModel model)
        {
            if (model.StartIndex < 0 || model.EndIndex < 0)
            {
                throw new FibonacciException(HttpStatusCode.BadRequest, $"{nameof(model)} is invalid");
            }
            
            if (model.UseCache)
            {
                if (_cache.TryGetValue($"{model.StartIndex}-{model.EndIndex}", out List<int> cashedNumbers))
                {
                    return cashedNumbers;
                }
            }
            
            var numbers = new List<int>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            for (var i = model.StartIndex; i <= model.EndIndex; i++)
            {
                var totalMemory = GC.GetTotalAllocatedBytes().ConvertBytesToMegabytes();
                if (totalMemory < _applicationSettings.MemoryLimit)
                {
                    throw new FibonacciException(HttpStatusCode.BadRequest,
                        $"{nameof(totalMemory)} is {totalMemory} Bytes. Memory limit is {_applicationSettings.MemoryLimit}");

                    // Uncomment this in case of we want to return already calculated data.
                    // return numbers;
                }
                
                if (stopwatch.Elapsed.Minutes > _applicationSettings.RunningTime)
                {
                    stopwatch.Stop();
                    throw new FibonacciException(HttpStatusCode.BadRequest,
                        $"{nameof(_applicationSettings.RunningTime)} is {_applicationSettings.RunningTime} minutes. Time elapsed {stopwatch.Elapsed.Minutes}");

                    // Uncomment this in case of we want to return already calculated data.
                    //return numbers;
                }

                numbers.Add(Fibonacci(i));
            }

            
            if (model.UseCache)
            {
                _cache.Set($"{model.StartIndex}-{model.EndIndex}", numbers, _cacheItemPolicy.AbsoluteExpiration);
            }
            
            return numbers;
        }
        
        private static int Fibonacci(int n)
        {
            var a = 0;
            var b = 1;
            
            for (var i = 0; i < n; i++)
            {
                var temp = a;
                a = b;
                b = temp + b;
            }
            
            return a;
        }
    }
}
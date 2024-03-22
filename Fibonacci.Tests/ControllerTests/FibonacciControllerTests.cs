using Fibonacci.Api.Controllers;
using Fibonacci.Api.RequestModels;
using Fibonacci.Service.Model;
using Fibonacci.Service.Model.Error;
using Fibonacci.Service.Services;
using Fibonacci.Tests.Base;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Fibonacci.Tests.ControllerTests
{
    public class FibonacciControllerTests : BaseTest
    {
        private readonly FibonacciController _fibonacciController;

        public FibonacciControllerTests()
        {
            var memoryCache = GetMemoryCache(null);
            var applicationOptions = Options.Create(new ApplicationSettings
            {
                CacheTime = 5,
                MemoryLimit = 10,
                RunningTime = 15,
            });
            
            var fibonacciNumbersService = new GetFibonacciNumbersService(memoryCache, applicationOptions);
            
            _fibonacciController = new FibonacciController(Mock.Of<ILogger<FibonacciController>>(), fibonacciNumbersService, Mapper);
        }

        private static IMemoryCache GetMemoryCache(object expectedValue)
        {
            var mockMemoryCache = new Mock<IMemoryCache>();
            
            mockMemoryCache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue))
                .Returns(true);
            
            return mockMemoryCache.Object;
        }

        [Fact(DisplayName = "Fibonacci Numbers: Should return Ok")]
        public async void Fibonacci_Controller_Should_Return_Ok_Async()
        {
            // Act
            var response = await _fibonacciController.GetFibonacciData(new FibonacciRequestModel
            {
                StartIndex = 2,
                EndIndex = 6,
                UseCache = true,
            });

                // Assert
            Assert.IsType<string>(response);
            Assert.NotEmpty(response);
        }
        
        [Fact(DisplayName = "Fibonacci Numbers: Should return Bad Request")]
        public async void Fibonacci_Controller_Should_Return_Bad_Request()
        {
            // Assert
            await Assert.ThrowsAsync<FibonacciException>(async () =>

                await _fibonacciController.GetFibonacciData(new FibonacciRequestModel
                {
                    StartIndex = -2,
                    EndIndex = -6,
                    UseCache = true,
                }));
        }
    }
}
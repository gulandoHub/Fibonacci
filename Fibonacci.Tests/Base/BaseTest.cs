using AutoMapper;
using Fibonacci.Api.Mapping;

namespace Fibonacci.Tests.Base
{
    public class BaseTest
    {
        protected static readonly IMapper Mapper;

        static BaseTest()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();
        }
    }
}
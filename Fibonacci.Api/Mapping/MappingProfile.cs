using AutoMapper;
using Fibonacci.Api.RequestModels;
using Fibonacci.Service.Model;

namespace Fibonacci.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FibonacciRequestModel, FibonacciModel>().ReverseMap();
        }
    }
}
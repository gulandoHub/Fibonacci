using System;
using System.Linq;
using Fibonacci.Api.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ValidationFilter : ActionFilterAttribute
    {
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var (key, value) in context.ActionArguments.ToDictionary(x => x.Key, y => y.Value))
            {
                if (value is FibonacciRequestModel requestModel)
                {
                    if (requestModel.EndIndex < 0 || requestModel.StartIndex < 0)
                    {
                        _logger.LogError($"{nameof(ValidationFilter)} - {nameof(OnActionExecutionAsync)} model is invalid");
                        context.Result = new BadRequestObjectResult(requestModel);
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
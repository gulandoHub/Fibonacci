using System;
using System.Net;

namespace Fibonacci.Service.Model.Error
{
    public class FibonacciException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public override string Message { get; }

        public FibonacciException(HttpStatusCode status, string message = default)
        {
            StatusCode = status;
            Message = message;
        }
    }
}
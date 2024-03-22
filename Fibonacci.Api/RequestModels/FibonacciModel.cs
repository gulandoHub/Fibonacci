namespace Fibonacci.Api.RequestModels
{
    public class FibonacciRequestModel
    {
        public int StartIndex { get; set; }
        
        public int EndIndex { get; set; }

        public bool UseCache { get; set; } = false;
    }
}
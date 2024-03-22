using System;

namespace Fibonacci.Service.Model
{
    public class FibonacciModel
    {
        public int StartIndex { get; set; }
        
        public int EndIndex { get; set; }
        
        public bool UseCache { get; set; }
    }
}
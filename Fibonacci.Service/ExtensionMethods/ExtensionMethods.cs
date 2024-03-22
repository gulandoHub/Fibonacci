namespace Fibonacci.Service.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static double ConvertBytesToMegabytes(this long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
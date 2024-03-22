namespace Fibonacci.Service.Model
{
    public class ApplicationSettings
    {
        /// <summary>
        /// CacheTime in minutes.
        /// </summary>
        public int CacheTime { get; set; }
        
        /// <summary>
        /// RunningTime in minutes
        /// </summary>
        public int RunningTime { get; set; }
        
        /// <summary>
        /// MemoryLimit in MB.
        /// </summary>
        public int MemoryLimit { get; set; }
    }
}
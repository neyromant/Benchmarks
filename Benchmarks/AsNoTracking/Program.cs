using BenchmarkDotNet.Running;

namespace AsNoTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AsNoTrackingTests>();
        }
    }
}

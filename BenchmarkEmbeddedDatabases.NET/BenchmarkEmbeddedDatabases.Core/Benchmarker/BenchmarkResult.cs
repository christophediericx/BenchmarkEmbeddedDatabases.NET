namespace BenchmarkEmbeddedDatabases.Core.Benchmarker
{
    public class BenchmarkResult
    {
        public string TechnologyIdentifier { get; set; }
        public string BenchMarkIdentifier { get; set; }
        public long Result { get; set; }
        public int Iterations { get; set; }
    }
}

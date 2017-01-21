using System.Data.Common;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public interface IBenchmark : IHaveIdentifier
    {
        int Iterations { get; }
        DbTransaction Transaction { get; set; }
        void Prepare();
        void Execute(int iteration);
        void TearDown();
    }
}

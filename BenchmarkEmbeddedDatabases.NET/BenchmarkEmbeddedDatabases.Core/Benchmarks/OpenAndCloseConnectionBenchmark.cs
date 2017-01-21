using System.Data;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class OpenAndCloseConnectionBenchmark : BaseBenchmark
    {
        public override string Identifier => 
            $"Open and close {Iterations} connections (default connection pooling)";

        public OpenAndCloseConnectionBenchmark(IBenchmarkHarness harness)
            : base(harness, 250)
        {
        }

        public override void Prepare()
        {
            // Noop
        }

        public override void Execute(int iteration)
        {
            using (var connection = Harness.CreateConnection())
            {
                connection.Open();           
                connection.State.Assert((state) => state.Equals(ConnectionState.Open));
                connection.Close();
                connection.State.Assert((state) => state.Equals(ConnectionState.Closed));
            }
        }

        public override void TearDown()
        {
            // Noop
        }
    }
}

using System;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class DeleteWithoutIndexBenchmark : BaseBenchmark
    {
        public override string Identifier =>
            $"Delete {Iterations} records - selected by non indexed field";

        private readonly Guid[] _guidsToDelete;

        public DeleteWithoutIndexBenchmark
            (IBenchmarkHarness harness, int iterations, Guid[] guidsToDelete) 
            : base(harness, iterations)
        {
            _guidsToDelete = guidsToDelete;
        }

        public override void Execute(int iteration)
        {
            var guid = _guidsToDelete[iteration];
            Command.CommandText = $"delete from t2 where field1 = '{guid}'";
            Command.ExecuteNonQuery();
        }
    }
}

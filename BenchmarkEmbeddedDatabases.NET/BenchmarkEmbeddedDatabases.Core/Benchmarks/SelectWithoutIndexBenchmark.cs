using System;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class SelectWithoutIndexBenchmark : BaseBenchmark
    {
        public override string Identifier =>
            $"Fetch {Iterations} records - selected by a non indexed field";

        private readonly Guid[] _guidsToSelect;

        public SelectWithoutIndexBenchmark(IBenchmarkHarness harness, int iterations, Guid[] guidsToSelect)
            : base(harness, iterations)
        {
            _guidsToSelect = guidsToSelect;
        }

        public override void Execute(int iteration)
        {
            var guid = _guidsToSelect[iteration];
            Command.CommandText = $"select id from t1 where field1 = '{guid}'";
            var result = Command.ExecuteScalar();
            if (result == null)
                throw new ArgumentException(
                    $"Expected record in t1 with field1 = '{guid}' to exist!");
        }
    }
}

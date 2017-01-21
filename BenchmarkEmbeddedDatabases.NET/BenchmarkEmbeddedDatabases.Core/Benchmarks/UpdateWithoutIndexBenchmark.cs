using System;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class UpdateWithoutIndexBenchmark : BaseBenchmark
    {
        public override string Identifier =>
            $"Update {Iterations} records - selected by using a non indexed field";

        private readonly Guid[] _guidsToUpdate;

        public UpdateWithoutIndexBenchmark
            (IBenchmarkHarness harness, int iterations, Guid[] guidsToUpdate) 
            : base(harness, iterations)
        {
            _guidsToUpdate = guidsToUpdate;
        }

        public override void Execute(int iteration)
        {
            var guid = _guidsToUpdate[iteration];
            Command.CommandText = $"update t2 set field1 = '{Guid.NewGuid()}' where field1 = '{guid}'";
            var result = Command.ExecuteNonQuery();
            if (result == 0)
                throw new ArgumentException(
                    "Update of guid (without index) in t2 failed!");
        }
    }
}

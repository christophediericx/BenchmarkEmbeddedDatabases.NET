using System;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class InsertWithAutoIncPrimaryKeyBenchmark : BaseBenchmark
    {
        public override string Identifier => 
            $"Insert {Iterations} simple records - single transaction - autoinc pk";

        private readonly Guid[] _guids;

        public InsertWithAutoIncPrimaryKeyBenchmark(IBenchmarkHarness harness, int iterations, Guid[] guids)
            : base(harness, iterations)
        {
            _guids = guids;
        }

        public override void Execute(int iteration)
        {
            Command.CommandText = 
                $"insert into t1 (field1) values ('{_guids[iteration]}');";

            Command.ExecuteNonQuery();
        }
    }
}

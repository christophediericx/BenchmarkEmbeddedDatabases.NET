using System;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class InsertBenchmark : BaseBenchmark
    {
        public override string Identifier => 
            $"Insert {Iterations} simple records - single transaction";

        private readonly Guid[] _guids;
        private int _counter;

        public InsertBenchmark(IBenchmarkHarness harness, int iterations, Guid[] randomGuids)
            : base(harness, iterations)
        {
            _guids = randomGuids;
        }

        public override void Prepare()
        {
            base.Prepare();
            _counter++;
        }

        public override void Execute(int iteration)
        {
            Command.CommandText = 
                $"insert into t2 (id, field1) values ({_counter++}, '{_guids[iteration]}');";

            Command.ExecuteNonQuery();
        }
    }
}

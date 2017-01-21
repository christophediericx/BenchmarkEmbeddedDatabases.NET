using System;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class UpdateOnPrimaryKeyBenchmark : BaseBenchmark
    {
        public override string Identifier =>
            $"Update {Iterations} records - selected by (indexed) primary key";

        private readonly int[] _primaryKeysToUpdate;

        public UpdateOnPrimaryKeyBenchmark
            (IBenchmarkHarness harness, int iterations, int[] primaryKeysToUpdate) 
            : base(harness, iterations)
        {
            _primaryKeysToUpdate = primaryKeysToUpdate;
        }

        public override void Execute(int iteration)
        {
            var id = _primaryKeysToUpdate[iteration];
            Command.CommandText = $"update t1 set field1 = '{Guid.NewGuid()}' where id = {id}";
            var result = Command.ExecuteNonQuery();
            if (result == 0)
                throw new ArgumentException(
                    "Update of guid (selected on primary key) in t1 failed!");
        }
    }
}

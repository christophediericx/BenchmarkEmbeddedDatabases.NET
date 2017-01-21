using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class DeleteOnPrimaryKeyBenchmark : BaseBenchmark
    {
        public override string Identifier =>
            $"Delete {Iterations} records - selected by (indexed) primary key";

        private readonly int[] _primaryKeysToDelete;

        public DeleteOnPrimaryKeyBenchmark(IBenchmarkHarness harness, int iterations, int[] primaryKeysToDelete) 
            : base(harness, iterations)
        {
            _primaryKeysToDelete = primaryKeysToDelete;
        }

        public override void Execute(int iteration)
        {
            var id = _primaryKeysToDelete[iteration];
            Command.CommandText = $"delete from t1 where id = {id}";
            Command.ExecuteNonQuery();
        }
    }
}

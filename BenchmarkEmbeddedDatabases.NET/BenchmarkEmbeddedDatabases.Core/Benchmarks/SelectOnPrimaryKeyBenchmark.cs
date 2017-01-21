using System;
using System.Linq;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public class SelectOnPrimaryKeyBenchmark : BaseBenchmark
    {
        public override string Identifier =>
            $"Fetch {Iterations} records - selected by (indexed) primary key";

        private int[] _idsToSelect;

        public SelectOnPrimaryKeyBenchmark(IBenchmarkHarness harness, int iterations, int[] idsToSelect)
            : base(harness, iterations)
        {
            _idsToSelect = idsToSelect;
        }

        public override void Prepare()
        {
            base.Prepare();
            _idsToSelect = _idsToSelect.Take(Iterations).ToArray();
        }

        public override void Execute(int iteration)
        {
            var id = _idsToSelect[iteration];
            Command.CommandText = $"select field1 from t1 where id = {id}";
            var result = Command.ExecuteScalar();
            if (result == null)
                throw new ArgumentException(
                    $"Expected record in t1 with id = '{id}' to exist!");
        }
    }
}


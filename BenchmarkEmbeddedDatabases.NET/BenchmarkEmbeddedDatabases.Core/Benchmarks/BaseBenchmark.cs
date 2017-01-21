using System.Data;
using System.Data.Common;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public abstract class BaseBenchmark : IBenchmark
    {
        public abstract string Identifier { get; }

        public int Iterations { get; }

        protected IBenchmarkHarness Harness { get; }
        protected DbConnection Connection { get; private set; }
        protected DbCommand Command { get; private set; }
        public DbTransaction Transaction { get; set; }

        protected BaseBenchmark(IBenchmarkHarness harness, int iterations)
        {
            Harness = harness;
            Iterations = iterations;
        }

        public virtual void Prepare()
        {
            Connection = Harness.CreateConnection();
            Connection.Open();
            Connection.State.Assert((state) => state.Equals(ConnectionState.Open));
            Command = Connection.CreateCommand();
            Transaction = Connection.BeginTransaction();
            Command.Transaction = Transaction;
        }

        public virtual void TearDown()
        {
            Connection.Close();
            Connection.State.Assert((state) => state.Equals(ConnectionState.Closed));
            Transaction.Dispose();
            Command.Dispose();
            Connection.Dispose();
        }

        public abstract void Execute(int iteration);
    }
}

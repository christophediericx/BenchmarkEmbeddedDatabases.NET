using System.Data.Common;
using NLog;

namespace BenchmarkEmbeddedDatabases.Core.BenchmarkHarness
{
    public abstract class BaseBenchmarkHarness : IBenchmarkHarness
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected string Path { get; }

        protected BaseBenchmarkHarness(string path)
        {
            Path = path;
        }

        public abstract string Identifier { get; }
        public abstract string ConnectionString { get; }
        public abstract DbConnection CreateConnection();
        public abstract void CreateDatabase();
        public abstract void CreateTestTables();
    }
}


using System.Data.Common;

namespace BenchmarkEmbeddedDatabases.Core.BenchmarkHarness
{
    public interface IBenchmarkHarness : IHaveIdentifier
    {
        string ConnectionString { get; }

        DbConnection CreateConnection();

        void CreateDatabase();
        void CreateTestTables();
    }
}

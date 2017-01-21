using System.Data.SqlClient;

namespace BenchmarkEmbeddedDatabases.LocalDB
{
    public class LocalDb2014BenchmarkHarness : LocalDbBenchmarkHarness
    {
        public override string Identifier => "SQL Server Express LocalDB 2014";

        public LocalDb2014BenchmarkHarness(string path) : base(path)
        {
            ConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\v12.0",
                IntegratedSecurity = true
            };
        }
    }
}

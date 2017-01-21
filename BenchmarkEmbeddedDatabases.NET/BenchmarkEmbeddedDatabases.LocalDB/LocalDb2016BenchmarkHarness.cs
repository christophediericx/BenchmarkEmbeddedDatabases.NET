using System.Data.SqlClient;

namespace BenchmarkEmbeddedDatabases.LocalDB
{
    public class LocalDb2016BenchmarkHarness : LocalDbBenchmarkHarness
    {
        public override string Identifier => "SQL Server Express LocalDB 2016";

        public LocalDb2016BenchmarkHarness(string path) : base(path)
        {
            ConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                IntegratedSecurity = true
            };
        }
    }
}

using System.Data.SqlClient;

namespace BenchmarkEmbeddedDatabases.LocalDB
{
    public class LocalDb2012BenchmarkHarness : LocalDbBenchmarkHarness
    {
        public override string Identifier => "SQL Server Express LocalDB 2012";

        public LocalDb2012BenchmarkHarness(string path) 
            : base(path)
        {
            ConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\v11.0",
                IntegratedSecurity = true
            };
        }
    }
}

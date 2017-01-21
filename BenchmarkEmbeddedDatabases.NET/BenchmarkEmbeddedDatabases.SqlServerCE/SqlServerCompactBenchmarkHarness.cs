using System.Data.Common;
using System.Data.SqlServerCe;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;
using BenchmarkEmbeddedDatabases.Core.Benchmarks;

namespace BenchmarkEmbeddedDatabases.SqlServerCE
{
    public class SqlServerCompactBenchmarkHarness : BaseBenchmarkHarness
    {
        public override string Identifier => "SQL Server Compact 4.0.8876.1";
        public override string ConnectionString => $"Data Source=\"{Path}\";";

        public SqlServerCompactBenchmarkHarness(string path) 
            : base(path)
        {
        }

        public override DbConnection CreateConnection()
        {
            return new SqlCeConnection(ConnectionString);
        }

        public override void CreateDatabase()
        {
            var engine = new SqlCeEngine(ConnectionString);
            engine.CreateDatabase();
        }

        public override void CreateTestTables()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                connection.Execute("create table t1 (id int identity not null primary key, field1 nvarchar(255));");
                connection.Execute("create table t2 (id int not null primary key, field1 nvarchar(255));");
                connection.Close();
            }
        }
    }
}

using System.Data.Common;
using System.Data.SQLite;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;
using BenchmarkEmbeddedDatabases.Core.Benchmarks;

namespace BenchmarkEmbeddedDatabases.Sqlite
{
    public class SqliteBenchmarkHarness : BaseBenchmarkHarness
    {
        public override string Identifier => "System.Data.Sqlite (1.0.104)";
        public override string ConnectionString => $"Data Source={Path};Version=3";

        public SqliteBenchmarkHarness(string path) 
            : base(path)
        {
        }

        public override DbConnection CreateConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public override void CreateDatabase()
        {
            SQLiteConnection.CreateFile(Path);
        }

        public override void CreateTestTables()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                connection.Execute("create table t1 (id integer primary key, field1 varchar(255));");
                connection.Execute("create table t2 (id integer primary key, field1 varchar(255));");
                connection.Close();
            }
        }
    }
}

using System.Data.Common;
using System.Data.SqlClient;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;
using BenchmarkEmbeddedDatabases.Core.Benchmarks;

namespace BenchmarkEmbeddedDatabases.LocalDB
{
    public abstract class LocalDbBenchmarkHarness : BaseBenchmarkHarness
    {
        public override string ConnectionString
            => ConnectionStringBuilder != null ?
            ConnectionStringBuilder.ConnectionString : string.Empty;

        public SqlConnectionStringBuilder ConnectionStringBuilder { get; protected set; }

        private static string DatabaseName => "TestDatabase";

        protected LocalDbBenchmarkHarness(string path)
            : base(path)
        {
        }

        public override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public override void CreateDatabase()
        {
            ConnectionStringBuilder.InitialCatalog = "master";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                        $"if exists (select * from sys.databases where name = '{DatabaseName}') "
                        + $"exec sp_detach_db '{DatabaseName}'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =
                        $"create database {DatabaseName} on (name = N'{DatabaseName}', filename = '{Path}')";
                    cmd.ExecuteNonQuery();
                }
            }

            ConnectionStringBuilder.InitialCatalog = DatabaseName;
            ConnectionStringBuilder.AttachDBFilename = Path;
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

using System.Data.Common;
using System.Data.OleDb;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;
using BenchmarkEmbeddedDatabases.Core.Benchmarks;

namespace BenchmarkEmbeddedDatabases.Access2016
{
    public abstract class AccessBenchmarkHarness : BaseBenchmarkHarness
    {
        protected AccessBenchmarkHarness(string path) : base(path)
        {
        }

        public override DbConnection CreateConnection()
        {
            return new OleDbConnection(ConnectionString);
        }

        public override void CreateDatabase()
        {
            var cat = new ADOX.Catalog();
            cat.Create(ConnectionString);
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

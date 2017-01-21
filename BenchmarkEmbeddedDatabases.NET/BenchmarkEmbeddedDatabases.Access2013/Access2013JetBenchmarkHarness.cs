namespace BenchmarkEmbeddedDatabases.Access2016
{
    public class Access2013JetBenchmarkHarness : AccessBenchmarkHarness
    {
        public override string Identifier => "Access 2013 32-bit (JET OLEDB 4)";
        public override string ConnectionString => $"Provider=Microsoft.JET.OLEDB.4.0;Data Source={Path};";

        public Access2013JetBenchmarkHarness(string path) : base(path)
        {
        }
    }
}

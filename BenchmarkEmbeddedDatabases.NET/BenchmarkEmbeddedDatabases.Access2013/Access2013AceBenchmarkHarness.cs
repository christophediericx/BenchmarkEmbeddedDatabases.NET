namespace BenchmarkEmbeddedDatabases.Access2016
{
    public class Access2013AceBenchmarkHarness : AccessBenchmarkHarness
    {
        public override string Identifier => "Access 2013 32-bit (ACE OLEDB 15)";
        public override string ConnectionString => $"Provider=Microsoft.ACE.OLEDB.15.0;Data Source={Path};";

        public Access2013AceBenchmarkHarness(string path) : base(path)
        {
        }
    }
}

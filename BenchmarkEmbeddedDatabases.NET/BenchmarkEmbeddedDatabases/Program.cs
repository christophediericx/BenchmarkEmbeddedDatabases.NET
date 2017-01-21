using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkEmbeddedDatabases.Access2016;
using BenchmarkEmbeddedDatabases.Core.Benchmarker;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;
using BenchmarkEmbeddedDatabases.Core.Benchmarks;
using BenchmarkEmbeddedDatabases.LocalDB;
using BenchmarkEmbeddedDatabases.Sqlite;
using BenchmarkEmbeddedDatabases.SqlServerCE;
using NLog;

namespace BenchmarkEmbeddedDatabases
{
    internal class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            Logger.Info("Starting benchmark of embedded databases...");

            const int numberOfRecordsToInsert = 25000;

            const int numberOfPrimaryKeysToSelect = 10000;
            const int numberOfPrimaryKeysToUpdate = 10000;
            const int numberOfPrimaryKeysToDelete = 10000;
            const int numberOfGuidsToSelect = 2000;
            const int numberOfGuidsToUpdate = 2000;
            const int numberOfGuidsToDelete = 2000;

            // Prepare a set of 50000 guids.
            var guids = new Guid[numberOfRecordsToInsert];
            for (var i = 0; i < guids.Length; i++)
                guids[i] = Guid.NewGuid();

            var primaryKeys = Enumerable.Range(1, numberOfRecordsToInsert).ToArray();

            var primaryKeysToSelect = primaryKeys.TakeRandomSlice(numberOfPrimaryKeysToSelect);
            var primaryKeysToUpdate = primaryKeys.TakeRandomSlice(numberOfPrimaryKeysToUpdate);
            var primaryKeysToDelete = primaryKeys.TakeRandomSlice(numberOfPrimaryKeysToDelete);

            var guidsToSelect = guids.TakeRandomSlice(numberOfGuidsToSelect);
            var guidsToUpdate = guids.TakeRandomSlice(numberOfGuidsToUpdate);
            var guidsToDelete = guids.TakeRandomSlice(numberOfGuidsToDelete);

            IList<Func<string, IBenchmarkHarness>> benchmarkHarnesses = 
                new List<Func<string, IBenchmarkHarness>>
                {
                    (path) => new Access2013AceBenchmarkHarness(path),
                    (path) => new Access2013JetBenchmarkHarness(path),
                    (path) => new LocalDb2012BenchmarkHarness(path),
                    (path) => new LocalDb2014BenchmarkHarness(path),
                    (path) => new LocalDb2016BenchmarkHarness(path),
                    (path) => new SqliteBenchmarkHarness(path),
                    (path) => new SqlServerCompactBenchmarkHarness(path)
                };

            IList<Func<IBenchmarkHarness, IBenchmark>> benchmarks = 
                new List<Func<IBenchmarkHarness, IBenchmark>>
                {
                    (harness) => new OpenAndCloseConnectionBenchmark(harness),
                    (harness) => new InsertBenchmark(harness, numberOfRecordsToInsert, guids),
                    (harness) => new InsertWithAutoIncPrimaryKeyBenchmark(harness, numberOfRecordsToInsert, guids),
                    (harness) => new SelectOnPrimaryKeyBenchmark(harness, numberOfPrimaryKeysToSelect, primaryKeysToSelect),
                    (harness) => new SelectWithoutIndexBenchmark(harness, numberOfGuidsToSelect, guidsToSelect),
                    (harness) => new UpdateOnPrimaryKeyBenchmark(harness, numberOfPrimaryKeysToUpdate, primaryKeysToUpdate),
                    (harness) => new UpdateWithoutIndexBenchmark(harness, numberOfGuidsToUpdate, guidsToUpdate),
                    (harness) => new DeleteOnPrimaryKeyBenchmark(harness, numberOfPrimaryKeysToDelete, primaryKeysToDelete),
                    (harness) => new DeleteWithoutIndexBenchmark(harness, numberOfGuidsToDelete, guidsToDelete)
                };

            var benchmarkResults = 
                EmbeddedDatabaseBenchmarker.Benchmark(benchmarkHarnesses, benchmarks);

            SummarizeResults(benchmarkResults);
        }

        private static void SummarizeResults(IEnumerable<BenchmarkResult> benchmarkResults)
        {
            var benchmarks = benchmarkResults.GroupBy(x => x.BenchMarkIdentifier);
            foreach (var grouping in benchmarks)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Logger.Info(string.Empty);
                Logger.Info($"Benchmark '{grouping.Key}'");
                Logger.Info(string.Empty);

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var item in grouping.OrderBy(x => x.Result))
                {
                    var avgInMicroSecond = (item.Result / (double) item.Iterations) * 1000;
                    Logger.Info("{0:D6} ms - AVG {1:000000} μs - {2}", item.Result, 
                        avgInMicroSecond, item.TechnologyIdentifier);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

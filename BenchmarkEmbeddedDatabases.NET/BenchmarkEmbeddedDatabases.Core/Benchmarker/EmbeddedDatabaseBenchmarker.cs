using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using BenchmarkEmbeddedDatabases.Core.BenchmarkHarness;
using BenchmarkEmbeddedDatabases.Core.Benchmarks;
using NLog;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarker
{
    public static class EmbeddedDatabaseBenchmarker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static IList<BenchmarkResult> Benchmark(IList<Func<string, IBenchmarkHarness>> benchmarkHarnesses,
            IList<Func<IBenchmarkHarness, IBenchmark>> benchmarks)
        {
            // Use the second Core/Processor for the test
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);

            // Prevent "Normal" Processes from interrupting Threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            // Prevent "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            var results = new List<BenchmarkResult>();
            foreach (var harnessFunc in benchmarkHarnesses)
            {
                var dbPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.tmp");
                var harness = harnessFunc(dbPath);
                var harnessIdentifier = harness.Identifier;
                Logger.Debug($"Harness for '{harnessIdentifier}' initialized...");

                try
                {
                    Logger.Info($"Creating initial database ({dbPath})...");
                    harness.CreateDatabase();
                    harness.CreateTestTables();

                    foreach (var benchmarkFunc in benchmarks)
                    {
                        var benchmark = benchmarkFunc(harness);
                        var testSetSize = benchmark.Iterations;
                        var benchmarkIdentifier = benchmark.Identifier;

                        Logger.Info($"Running benchmark '{benchmarkIdentifier}'...");

                        var stopwatch = new Stopwatch();
                        benchmark.Prepare();

                        stopwatch.Start();

                        for (var i = 0; i < testSetSize; i++)
                            benchmark.Execute(i);

                        benchmark.Transaction?.Commit();

                        stopwatch.Stop();
                        benchmark.TearDown();

                        var timeElapsed = stopwatch.ElapsedMilliseconds;
                        Logger.Info($"Benchmark executed in {timeElapsed} ms.");
                        results.Add(new BenchmarkResult
                        {
                            TechnologyIdentifier = harnessIdentifier,
                            BenchMarkIdentifier = benchmarkIdentifier,
                            Result = timeElapsed,
                            Iterations = testSetSize
                        });
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"Exception happened during benchmark of '{harnessIdentifier}'");
                    Logger.Error(ex.Message);
                    Logger.Error(ex.StackTrace);
                    return null;
                }
                finally
                {
                    if (File.Exists(dbPath))
                    {
                        Logger.Debug($"Removing '{dbPath}'.");
                        try
                        {
                            File.Delete(dbPath);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex, $"Unable to remove database '{dbPath}'.");
                        }
                    }
                }
            }
            return results;
        }
    }
}

using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace BenchmarkEmbeddedDatabases.Core.Benchmarks
{
    public static class Extensions
    {
        public static void Execute(this DbConnection connection, string sql)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public static void Assert(this ConnectionState connectionState, Func<ConnectionState, bool> func)
        {
            if (!func(connectionState))
                throw new ArgumentException("Assertion failed!");
        }

        public static void Assert(this string s, Func<string, bool> func)
        {
            if(!func(s))
                throw new ArgumentException("Assertion failed!");
        }


        public static T[] TakeRandomSlice<T>(this T[] array, int sliceSize)
        {
            var len = array.Length;
            var copy = new T[len];
            Array.Copy(array, copy, len);
            copy.Shuffle();
            return copy.Take(sliceSize).ToArray();
        }

        public static void Shuffle<T>(this T[] array)
        {
            var n = array.Length;
            while (n > 1)
            {
                n--;
                var k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
        }
    }
}

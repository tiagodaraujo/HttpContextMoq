using System;
using System.Threading;
using Microsoft.Extensions.Primitives;

namespace HttpContextMoq.Tests
{
    public static class Fakes
    {
        private readonly static Random Random = new Random();

        // Primitives
        public static string String = new Guid().ToString();
        public static int Int = Random.Next();
        public static long Long = Random.Next();
        internal static bool Bool = true;

        // Structs
        public static string StringValues = new StringValues(String);
        internal static CancellationToken CancellationToken = new CancellationToken();

        // Out Variables
        public static StringValues OutStringValues;
    }
}

using System;
using System.Threading;
using Microsoft.Extensions.Primitives;

namespace HttpContextMoq.Tests
{
    public static class Fakes
    {
        private readonly static Random Random = new Random();

        // Primitives
        public static bool Bool = true;
        public static byte[] ByteArray = new byte[] { };
        public static int Int = Random.Next();
        public static long Long = Random.Next();
        public static string String = new Guid().ToString();

        // Structs
        public static CancellationToken CancellationToken = new CancellationToken();
        public static string StringValues = new StringValues(String);

        // Classes
        public static object Object = new object();
        public static readonly Type Type = typeof(Fakes);

        // Out Variables
        public static byte[] OutByteArray;
        public static object OutObject;
        public static string OutString;
        public static StringValues OutStringValues;
    }
}

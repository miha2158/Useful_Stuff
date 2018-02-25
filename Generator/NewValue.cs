using System;

namespace Generator
{
    public static class NewValue
    {
        private static readonly Random Rand = new Random();

        public static bool Boolean => Rand.Next(2) == 0;

        public static int Int() => Rand.Next();
        public static int Int(int max) => Rand.Next(max);
        public static int Int(int min, int max) => min != max ? Rand.Next(min, max) : min;

        public static long Long()
        {
            var b = new byte[8];
            Rand.NextBytes(b);
            return BitConverter.ToInt64(b, 0);
        }
        public static long Long(long max) => Long() % max;
        public static long Long(long min, long max) => min != max? Long() % (max - min) + min: min;

        public static DateTime NewDateTime(DateTime min, DateTime max) => new DateTime(Long(min.Ticks, max.Ticks));
    }
}

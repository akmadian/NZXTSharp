using System;
using NZXTSharp;

namespace TestApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            NZXTSharp.RateLimiter limiter = new NZXTSharp.RateLimiter(100);

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
                limiter.PerformAction();
            }
        }
    }
}

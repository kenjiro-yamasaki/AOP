using System;

namespace PostSharp.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            new LoggerTest().Test("BBB");
        }
    }

    class LoggerTest
    {
        public LoggerTest()
        {
        }

        [Logger]
        public void Test(string message)
        {
            Console.WriteLine(message);
        }
    }


}

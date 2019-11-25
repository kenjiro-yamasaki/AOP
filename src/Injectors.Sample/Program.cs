using System;
using System.Reflection;

namespace SoftCube.Injectors.Sample
{
    /// <summary>
    /// プログラム。
    /// </summary>
    class Program
    {
        /// <summary>
        /// メイン関数。
        /// </summary>
        /// <param name="args">アプリケーション引数</param>
        static void Main(string[] args)
        {
            new LoggerTest().Test("BBB", 101, DateTime.Now);
            //Test(null);

            Console.Read();
        }
    }

    class LoggerTest
    {
        public LoggerTest()
        {
        }

        [LoggerAspect]
        public void Test(string message, int number, DateTime now)
        {
            Console.WriteLine(message);

            //var aspect = new LoggerAspect();
            //var args = new MethodExecutionArgs(this, new Arguments(message, number, now));
            //args.Method = MethodBase.GetCurrentMethod();

            //aspect.OnEntry(args);
            //Console.WriteLine(message);
            //aspect.OnExit(args);





            //var aspect = new LoggerAspect();
            //aspect.OnEntry();
            //try
            //{
            //    Console.WriteLine(message);
            //}
            //finally
            //{
            //    aspect.OnExit();
            //}
        }
    }

}

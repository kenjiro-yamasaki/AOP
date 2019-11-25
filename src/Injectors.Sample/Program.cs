using System;

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
            new LoggerTest().Test("BBB");
            //Test(null);

            Console.Read();
        }

        //static void Test(string name)
        static void Test([NotNull]string name)
        {
        }
    }

    class LoggerTest
    {
        public LoggerTest()
        {
        }

        [LoggerAspect]
        public void Test(string message)
        {
            Console.WriteLine(message);

            ////var aspect = new LoggerAspect();
            ////aspect.OnEntry();
            ////Console.WriteLine(message);
            ////aspect.OnExit();

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

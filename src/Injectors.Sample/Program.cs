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
            Console.WriteLine("Hello World!");

            Test(null);
            Console.Read();
        }


        //static void Test(string name)
        static void Test([NotNull]string name)
        {
        }

    }
}

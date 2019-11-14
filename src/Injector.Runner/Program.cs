using System.IO;

namespace SoftCube.Injector.Runner
{
    /// <summary>
    /// プログラム。
    /// </summary>
    class Program
    {
        #region メソッド

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">アプリケーション引数</param>
        static void Main(string[] args)
        {
            var baseDirectory        = @"C:\Users\kenji\Projects\Kernel\src\Injector.Sample\bin\x64\Debug\net4.7.2";

            var targetAssemblyName   = "SoftCube.Aspect.Sample.exe";
            var targetAssemblyPath   = Path.Combine(baseDirectory, targetAssemblyName);

            var injectedAssemblyName = "SoftCube.Aspect.Sample_Injected.exe";
            var injectedAssemblyPath = Path.Combine(baseDirectory, injectedAssemblyName);

            var runner = new InjectorRunner();
            runner.Run(targetAssemblyPath, injectedAssemblyPath);
        }

        #endregion
    }
}

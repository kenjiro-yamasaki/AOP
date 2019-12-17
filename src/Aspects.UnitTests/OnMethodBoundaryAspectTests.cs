using SoftCube.Loggers;
using System;
using Xunit;

namespace SoftCube.Aspects.UnitTests
{
    /// <summary>
    /// OnMethodBoundaryAspectの単体テスト。
    /// </summary>
    public class OnMethodBoundaryAspectTests
    {
        #region メソッド


        [Fact]
        public void Test1()
        {
            var handler = new LogStringHandler();
            handler.ConversionPattern = "{message}{newline}";
            Logger.Add(handler);

            var aspectTest = new AspectTest();
            aspectTest.Test();

            Assert.Equal($"OnEntry{Environment.NewLine}OnSuccess{Environment.NewLine}OnExit{Environment.NewLine}", handler.ToString());
        }

        #endregion
    }

    public class AspectTest
    {
        [LoggerAspect]
        public void Test()
        {
            Console.WriteLine($"Hellow World");
        }
    }
}

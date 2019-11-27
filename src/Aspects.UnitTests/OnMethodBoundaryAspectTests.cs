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
            var aspectTest = new AspectTest();


            aspectTest.Test();
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

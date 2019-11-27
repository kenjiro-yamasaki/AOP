using System;
using Xunit;

namespace SoftCube.Aspects.UnitTests
{
    /// <summary>
    /// OnMethodBoundaryAspect�̒P�̃e�X�g�B
    /// </summary>
    public class OnMethodBoundaryAspectTests
    {
        #region ���\�b�h


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

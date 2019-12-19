using SoftCube.Logger;
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
            var appender = new StringAppender();
            appender.ConversionPattern = "{message}{newline}";
            Logger.Logger.Add(appender);

            var aspectTest = new AspectTest();
            aspectTest.Test();

            Assert.Equal($"OnEntry{Environment.NewLine}OnSuccess{Environment.NewLine}OnExit{Environment.NewLine}", appender.ToString());
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

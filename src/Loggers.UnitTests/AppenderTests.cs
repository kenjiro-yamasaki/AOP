using NSubstitute;
using System;
using System.Diagnostics;
using System.Threading;
using Xunit;

namespace SoftCube.Loggers.UnitTests
{
    public class AppenderTests
    {
        public class MinLevel
        {
            [Fact]
            public void Trace_Trace�ȏ���o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.Received(1).Log("Trace");
                appender.Received(1).Log("Debug");
                appender.Received(1).Log("Info");
                appender.Received(1).Log("Warning");
                appender.Received(1).Log("Error");
                appender.Received(1).Log("Fatal");
            }

            [Fact]
            public void Debug_Debug�ȏ���o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Debug, Level.Fatal);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.DidNotReceive().Log("Trace");
                appender.Received(1).Log("Debug");
                appender.Received(1).Log("Info");
                appender.Received(1).Log("Warning");
                appender.Received(1).Log("Error");
                appender.Received(1).Log("Fatal");
            }

            [Fact]
            public void Info_Info�ȏ���o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Info, Level.Fatal);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.DidNotReceive().Log("Trace");
                appender.DidNotReceive().Log("Debug");
                appender.Received(1).Log("Info");
                appender.Received(1).Log("Warning");
                appender.Received(1).Log("Error");
                appender.Received(1).Log("Fatal");
            }

            [Fact]
            public void Warning_Warning�ȏ���o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Warning, Level.Fatal);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.DidNotReceive().Log("Trace");
                appender.DidNotReceive().Log("Debug");
                appender.DidNotReceive().Log("Info");
                appender.Received(1).Log("Warning");
                appender.Received(1).Log("Error");
                appender.Received(1).Log("Fatal");
            }

            [Fact]
            public void Error_Error�ȏ���o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Error, Level.Fatal);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.DidNotReceive().Log("Trace");
                appender.DidNotReceive().Log("Debug");
                appender.DidNotReceive().Log("Info");
                appender.DidNotReceive().Log("Warning");
                appender.Received(1).Log("Error");
                appender.Received(1).Log("Fatal");
            }

            [Fact]
            public void Fatal_Fatal�ȏ���o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Fatal, Level.Fatal);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.DidNotReceive().Log("Trace");
                appender.DidNotReceive().Log("Debug");
                appender.DidNotReceive().Log("Info");
                appender.DidNotReceive().Log("Warning");
                appender.DidNotReceive().Log("Error");
                appender.Received(1).Log("Fatal");
            }
        }

        public class MaxLevel
        {
            [Fact]
            public void Fatal_Fatal�ȉ����o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.Received(1).Log("Trace");
                appender.Received(1).Log("Debug");
                appender.Received(1).Log("Info");
                appender.Received(1).Log("Warning");
                appender.Received(1).Log("Error");
                appender.Received(1).Log("Fatal");
            }

            [Fact]
            public void Error_Error�ȉ����o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Error);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.Received(1).Log("Trace");
                appender.Received(1).Log("Debug");
                appender.Received(1).Log("Info");
                appender.Received(1).Log("Warning");
                appender.Received(1).Log("Error");
                appender.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Warning_Warning�ȉ����o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Warning);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.Received(1).Log("Trace");
                appender.Received(1).Log("Debug");
                appender.Received(1).Log("Info");
                appender.Received(1).Log("Warning");
                appender.DidNotReceive().Log("Error");
                appender.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Info_Info�ȉ����o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Info);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.Received(1).Log("Trace");
                appender.Received(1).Log("Debug");
                appender.Received(1).Log("Info");
                appender.DidNotReceive().Log("Warning");
                appender.DidNotReceive().Log("Error");
                appender.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Debug_Debug�ȉ����o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Debug);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.Received(1).Log("Trace");
                appender.Received(1).Log("Debug");
                appender.DidNotReceive().Log("Info");
                appender.DidNotReceive().Log("Warning");
                appender.DidNotReceive().Log("Error");
                appender.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Trace_Trace�ȉ����o�͂���()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Trace);

                appender.Trace("Trace");
                appender.Debug("Debug");
                appender.Info("Info");
                appender.Warning("Warning");
                appender.Error("Error");
                appender.Fatal("Fatal");

                appender.Received(1).Log("Trace");
                appender.DidNotReceive().Log("Debug");
                appender.DidNotReceive().Log("Info");
                appender.DidNotReceive().Log("Warning");
                appender.DidNotReceive().Log("Error");
                appender.DidNotReceive().Log("Fatal");
            }
        }

        public class ConversionPattern
        {
            [Fact]
            public void Date_�������o�͂���()
            {
                var appender = new StringAppender("{date}");

                appender.Trace("A");

                var actual = appender.ToString();
                Assert.True(System.DateTime.TryParse(actual, out _));
            }

            [Fact]
            public void Date�����w��_�������o�͂���()
            {
                var appender = new StringAppender("{date:yyyy-MM-dd HH:mm:ss,fff}");

                appender.Trace("A");

                var actual = appender.ToString();
                Assert.Matches(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3}", actual);
            }

            [Fact]
            public void File_�������o�͂���()
            {
                var appender = new StringAppender("{file}");

                appender.Trace("A");

                var expected = new StackFrame(true).GetFileName();
                var actual   = appender.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Level_�������o�͂���()
            {
                var appender = new StringAppender("{level}");

                appender.Info("A");

                var expected = "INFO";
                var actual   = appender.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Level���l��_�������o�͂���()
            {
                var appender = new StringAppender("{level,-5}");

                appender.Info("A");

                var expected = "INFO ";
                var actual   = appender.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Level�E�l��_�������o�͂���()
            {
                var appender = new StringAppender("{level,5}");

                appender.Info("A");

                var log = appender.ToString();
                Assert.Equal(" INFO", log);
            }

            [Fact]
            public void Line_�������o�͂���()
            {
                var appender = new StringAppender("{line}");

                appender.Trace("A");

                var expected = (new StackFrame(true).GetFileLineNumber() - 2).ToString();
                var actual   = appender.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Message_�������o�͂���()
            {
                var appender = new StringAppender("{message}");

                appender.Info("A");

                var log = appender.ToString();
                Assert.Equal("A", log);
            }

            [Fact]
            public void Method_�������o�͂���()
            {
                var appender = new StringAppender("{method}");

                appender.Trace("A");

                var expected = nameof(Method_�������o�͂���);
                var actual   = appender.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void NewLine_�������o�͂���()
            {
                var appender = new StringAppender("{newline}");

                appender.Info("A");

                var log = appender.ToString();
                Assert.Equal($"{Environment.NewLine}", log);
            }

            [Fact]
            public void Thread_�������o�͂���()
            {
                var appender = new StringAppender("{thread}");

                appender.Trace("A");

                var expected = Thread.CurrentThread.ManagedThreadId.ToString();
                var actual   = appender.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Type_�������o�͂���()
            {
                var appender = new StringAppender("{type}");

                appender.Trace("A");

                var expected = new StackFrame(true).GetMethod().DeclaringType.FullName;
                var actual   = appender.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ��������_�������o�͂���()
            {
                var appender = new StringAppender("{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}");

                appender.Info("A");

                var log = appender.ToString();
                Assert.Matches(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3} \[INFO \] - A" + Environment.NewLine, log);
            }

            [Fact]
            public void �s���ȏ���_�𓊂���()
            {
                var appender = new StringAppender("{datetime}");

                var ex = Record.Exception(() => appender.Trace("A"));

                Assert.IsType<InvalidOperationException>(ex);
                Assert.Equal("ConversionPattern[{datetime}]���s���ł��B", ex.Message);
            }
        }

        public class Trace
        {
            [Fact]
            public void null_ArgumentNullException����������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                var ex = Record.Exception(() => appender.Trace(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void ��_���e����()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Trace("");

                appender.Received().Log("");
            }

            [Fact]
            public void A_��������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Trace("A");

                appender.Received().Log("A");
            }
        }

        public class Debug
        {
            [Fact]
            public void null_ArgumentNullException����������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                var ex = Record.Exception(() => appender.Debug(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void ��_���e����()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Debug("");

                appender.Received().Log("");
            }

            [Fact]
            public void A_��������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Debug("A");

                appender.Received().Log("A");
            }
        }

        public class Info
        {
            [Fact]
            public void null_ArgumentNullException����������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                var ex = Record.Exception(() => appender.Info(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void ��_���e����()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Info("");

                appender.Received().Log("");
            }

            [Fact]
            public void A_��������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Info("A");

                appender.Received().Log("A");
            }
        }

        public class Warning
        {
            [Fact]
            public void null_ArgumentNullException����������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                var ex = Record.Exception(() => appender.Warning(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void ��_���e����()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Warning("");

                appender.Received().Log("");
            }

            [Fact]
            public void A_��������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Warning("A");

                appender.Received().Log("A");
            }
        }

        public class Error
        {
            [Fact]
            public void null_ArgumentNullException����������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                var ex = Record.Exception(() => appender.Error(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void ��_���e����()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Error("");

                appender.Received().Log("");
            }

            [Fact]
            public void A_��������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Error("A");

                appender.Received().Log("A");
            }
        }

        public class Fatal
        {
            [Fact]
            public void null_ArgumentNullException����������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                var ex = Record.Exception(() => appender.Fatal(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void ��_���e����()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Fatal("");

                appender.Received().Log("");
            }

            [Fact]
            public void A_��������()
            {
                var appender = Substitute.For<Appender>("{message}", Level.Trace, Level.Fatal);

                appender.Fatal("A");

                appender.Received().Log("A");
            }
        }
    }
}

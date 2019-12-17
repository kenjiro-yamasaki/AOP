using NSubstitute;
using System;
using Xunit;

namespace SoftCube.Loggers.UnitTests
{
    public class LogHandlerTests
    {
        public class MinLevel
        {
            [Fact]
            public void Trace_Trace以上を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Trace;
                handler.MaxLevel          = LogLevel.Fatal;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.Received(1).Log("Trace");
                handler.Received(1).Log("Debug");
                handler.Received(1).Log("Info");
                handler.Received(1).Log("Warning");
                handler.Received(1).Log("Error");
                handler.Received(1).Log("Fatal");
            }

            [Fact]
            public void Debug_Debug以上を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Debug;
                handler.MaxLevel          = LogLevel.Fatal;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.DidNotReceive().Log("Trace");
                handler.Received(1).Log("Debug");
                handler.Received(1).Log("Info");
                handler.Received(1).Log("Warning");
                handler.Received(1).Log("Error");
                handler.Received(1).Log("Fatal");
            }

            [Fact]
            public void Info_Info以上を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Info;
                handler.MaxLevel          = LogLevel.Fatal;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.DidNotReceive().Log("Trace");
                handler.DidNotReceive().Log("Debug");
                handler.Received(1).Log("Info");
                handler.Received(1).Log("Warning");
                handler.Received(1).Log("Error");
                handler.Received(1).Log("Fatal");
            }

            [Fact]
            public void Warning_Warning以上を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Warning;
                handler.MaxLevel          = LogLevel.Fatal;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.DidNotReceive().Log("Trace");
                handler.DidNotReceive().Log("Debug");
                handler.DidNotReceive().Log("Info");
                handler.Received(1).Log("Warning");
                handler.Received(1).Log("Error");
                handler.Received(1).Log("Fatal");
            }

            [Fact]
            public void Error_Error以上を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Error;
                handler.MaxLevel          = LogLevel.Fatal;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.DidNotReceive().Log("Trace");
                handler.DidNotReceive().Log("Debug");
                handler.DidNotReceive().Log("Info");
                handler.DidNotReceive().Log("Warning");
                handler.Received(1).Log("Error");
                handler.Received(1).Log("Fatal");
            }

            [Fact]
            public void Fatal_Fatal以上を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Fatal;
                handler.MaxLevel          = LogLevel.Fatal;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.DidNotReceive().Log("Trace");
                handler.DidNotReceive().Log("Debug");
                handler.DidNotReceive().Log("Info");
                handler.DidNotReceive().Log("Warning");
                handler.DidNotReceive().Log("Error");
                handler.Received(1).Log("Fatal");
            }
        }

        public class MaxLevel
        {
            [Fact]
            public void Fatal_Fatal以下を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Trace;
                handler.MaxLevel          = LogLevel.Fatal;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.Received(1).Log("Trace");
                handler.Received(1).Log("Debug");
                handler.Received(1).Log("Info");
                handler.Received(1).Log("Warning");
                handler.Received(1).Log("Error");
                handler.Received(1).Log("Fatal");
            }

            [Fact]
            public void Error_Error以下を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Trace;
                handler.MaxLevel          = LogLevel.Error;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.Received(1).Log("Trace");
                handler.Received(1).Log("Debug");
                handler.Received(1).Log("Info");
                handler.Received(1).Log("Warning");
                handler.Received(1).Log("Error");
                handler.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Warning_Warning以下を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Trace;
                handler.MaxLevel          = LogLevel.Warning;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.Received(1).Log("Trace");
                handler.Received(1).Log("Debug");
                handler.Received(1).Log("Info");
                handler.Received(1).Log("Warning");
                handler.DidNotReceive().Log("Error");
                handler.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Info_Info以下を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Trace;
                handler.MaxLevel          = LogLevel.Info;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.Received(1).Log("Trace");
                handler.Received(1).Log("Debug");
                handler.Received(1).Log("Info");
                handler.DidNotReceive().Log("Warning");
                handler.DidNotReceive().Log("Error");
                handler.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Debug_Debug以下を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Trace;
                handler.MaxLevel          = LogLevel.Debug;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.Received(1).Log("Trace");
                handler.Received(1).Log("Debug");
                handler.DidNotReceive().Log("Info");
                handler.DidNotReceive().Log("Warning");
                handler.DidNotReceive().Log("Error");
                handler.DidNotReceive().Log("Fatal");
            }

            [Fact]
            public void Trace_Trace以下を出力する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.MinLevel          = LogLevel.Trace;
                handler.MaxLevel          = LogLevel.Trace;
                handler.ConversionPattern = "{message}";

                handler.Trace("Trace");
                handler.Debug("Debug");
                handler.Info("Info");
                handler.Warning("Warning");
                handler.Error("Error");
                handler.Fatal("Fatal");

                handler.Received(1).Log("Trace");
                handler.DidNotReceive().Log("Debug");
                handler.DidNotReceive().Log("Info");
                handler.DidNotReceive().Log("Warning");
                handler.DidNotReceive().Log("Error");
                handler.DidNotReceive().Log("Fatal");
            }
        }

        public class ConversionPattern
        {
            [Fact]
            public void Date_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "{date}";

                handler.Trace("A");

                var log = handler.ToString();
                Assert.True(System.DateTime.TryParse(log, out _));
            }

            [Fact]
            public void Date書式指定_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "{date:yyyy-MM-dd HH:mm:ss,fff}";

                handler.Trace("A");

                var log = handler.ToString();
                Assert.Matches(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3}", log);
            }

            [Fact]
            public void Level_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "[{level}]";

                handler.Info("A");

                var log = handler.ToString();
                Assert.Equal("[INFO]", log);
            }

            [Fact]
            public void Level左詰め_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "[{level,-5}]";

                handler.Info("A");

                var log = handler.ToString();
                Assert.Equal("[INFO ]", log);
            }

            [Fact]
            public void Level右詰め_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "[{level,5}]";

                handler.Info("A");

                var log = handler.ToString();
                Assert.Equal("[ INFO]", log);
            }

            [Fact]
            public void Message_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "{message}";

                handler.Info("A");

                var log = handler.ToString();
                Assert.Equal("A", log);
            }

            [Fact]
            public void NewLine_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "{newline}";

                handler.Info("A");

                var log = handler.ToString();
                Assert.Equal($"{Environment.NewLine}", log);
            }

            [Fact]
            public void 推奨書式_正しく出力する()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}";

                handler.Info("A");

                var log = handler.ToString();
                Assert.Matches(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3} \[INFO \] - A" + Environment.NewLine, log);
            }

            [Fact]
            public void 不正な書式_を投げる()
            {
                var handler = new LogStringHandler();
                handler.ConversionPattern = "{datetime}";

                var ex = Record.Exception(() => handler.Trace("A"));

                Assert.IsType<InvalidOperationException>(ex);
                Assert.Equal("ConversionPattern[{datetime}]が不正です。", ex.Message);
            }
        }

        public class Trace
        {
            [Fact]
            public void null_ArgumentNullExceptionが投げられる()
            {
                var handler = Substitute.For<LogHandler>();

                var ex = Record.Exception(() => handler.Trace(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void 空白_許容する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Trace("");

                handler.Received().Log("");
            }

            [Fact]
            public void A_成功する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Trace("A");

                handler.Received().Log("A");
            }
        }

        public class Debug
        {
            [Fact]
            public void null_ArgumentNullExceptionが投げられる()
            {
                var handler = Substitute.For<LogHandler>();

                var ex = Record.Exception(() => handler.Debug(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void 空白_許容する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Debug("");

                handler.Received().Log("");
            }

            [Fact]
            public void A_成功する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Debug("A");

                handler.Received().Log("A");
            }
        }

        public class Info
        {
            [Fact]
            public void null_ArgumentNullExceptionが投げられる()
            {
                var handler = Substitute.For<LogHandler>();

                var ex = Record.Exception(() => handler.Info(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void 空白_許容する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Info("");

                handler.Received().Log("");
            }

            [Fact]
            public void A_成功する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Info("A");

                handler.Received().Log("A");
            }
        }

        public class Warning
        {
            [Fact]
            public void null_ArgumentNullExceptionが投げられる()
            {
                var handler = Substitute.For<LogHandler>();

                var ex = Record.Exception(() => handler.Warning(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void 空白_許容する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Warning("");

                handler.Received().Log("");
            }

            [Fact]
            public void A_成功する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Warning("A");

                handler.Received().Log("A");
            }
        }

        public class Error
        {
            [Fact]
            public void null_ArgumentNullExceptionが投げられる()
            {
                var handler = Substitute.For<LogHandler>();

                var ex = Record.Exception(() => handler.Error(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void 空白_許容する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Error("");

                handler.Received().Log("");
            }

            [Fact]
            public void A_成功する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Error("A");

                handler.Received().Log("A");
            }
        }

        public class Fatal
        {
            [Fact]
            public void null_ArgumentNullExceptionが投げられる()
            {
                var handler = Substitute.For<LogHandler>();

                var ex = Record.Exception(() => handler.Fatal(null));

                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void 空白_許容する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Fatal("");

                handler.Received().Log("");
            }

            [Fact]
            public void A_成功する()
            {
                var handler = Substitute.For<LogHandler>();
                handler.ConversionPattern = "{message}";

                handler.Fatal("A");

                handler.Received().Log("A");
            }
        }
    }
}

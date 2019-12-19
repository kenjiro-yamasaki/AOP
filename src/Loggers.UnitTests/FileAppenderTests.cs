using System;
using System.IO;
using System.Text;
using Xunit;

namespace SoftCube.Loggers.UnitTests
{
    public class FileAppenderTests
    {
        public class Constructor
        {
            [Fact]
            public void appendがfalse_ログを上書きする()
            {
                var logFilePath = "test.log";
                CreateLogFile(logFilePath, "A");

                using (var appender = new FileAppender(logFilePath, append: false, Encoding.UTF8))
                {
                    appender.ConversionPattern = "{message}";
                    appender.Trace("B");
                }

                var actual = File.ReadAllText(logFilePath);
                Assert.Equal("B", actual);
            }

            [Fact]
            public void appendがtrue_ログを追加する()
            {
                var logFilePath = "test.log";
                CreateLogFile(logFilePath, "A");

                using (var appender = new FileAppender(logFilePath, append: true, Encoding.UTF8))
                {
                    appender.ConversionPattern = "{message}";
                    appender.Trace("B");
                }

                var actual = File.ReadAllText(logFilePath);
                Assert.Equal("AB", actual);
            }

            [Fact]
            public void filePathが不正なファイルパス_ArgumentExceptionを投げる()
            {
                Assert.Throws<ArgumentException>(() => new FileAppender("?.log", append: true, Encoding.UTF8));
            }

            [Fact]
            public void 引数がnull_ArgumentNullExceptionを投げる()
            {
                Assert.Throws<ArgumentNullException>(() => new FileAppender(null, append: true, Encoding.UTF8));
                Assert.Throws<ArgumentNullException>(() => new FileAppender("test.log", append: true, null));
            }

            void CreateLogFile(string logFilePath, string log)
            {
                using (var appender = new FileAppender(logFilePath, append: false, Encoding.UTF8))
                {
                    appender.ConversionPattern = "{message}";
                    appender.Trace(log);
                }
            }
        }

        public class Open
        {
            [Fact]
            public void appendがfalse_ログを上書きする()
            {
                var logFilePath = "test.log";
                CreateLogFile(logFilePath, "A");

                using (var appender = new FileAppender())
                {
                    appender.ConversionPattern = "{message}";
                    appender.Open(logFilePath, append: false, Encoding.UTF8);
                    appender.Trace("B");
                }

                var actual = File.ReadAllText(logFilePath);
                Assert.Equal("B", actual);
            }

            [Fact]
            public void appendがtrue_ログを追加する()
            {
                var logFilePath = "test.log";
                CreateLogFile(logFilePath, "A");

                using (var appender = new FileAppender())
                {
                    appender.ConversionPattern = "{message}";
                    appender.Open(logFilePath, append: true, Encoding.UTF8);
                    appender.Trace("B");
                }

                var actual = File.ReadAllText(logFilePath);
                Assert.Equal("AB", actual);
            }

            [Fact]
            public void 連続してOpenする_開いているファイルをCloseしてOpenする()
            {
                var appender = new FileAppender();
                appender.ConversionPattern = "{message}";
                appender.Open("A.log", append: false, Encoding.UTF8);
                appender.Trace("A");

                appender.Open("B.log", append: false, Encoding.UTF8);
                appender.Trace("B");
                appender.Close();

                Assert.Equal("A", File.ReadAllText("A.log"));
                Assert.Equal("B", File.ReadAllText("B.log"));
            }

            [Fact]
            public void filePathが不正なファイルパス_ArgumentExceptionを投げる()
            {
                Assert.Throws<ArgumentException>(() => new FileAppender().Open("?.log", append: true, Encoding.UTF8));
            }

            [Fact]
            public void 引数がnull_ArgumentNullExceptionを投げる()
            {
                Assert.Throws<ArgumentNullException>(() => new FileAppender().Open(null, append: true, Encoding.UTF8));
                Assert.Throws<ArgumentNullException>(() => new FileAppender().Open("test.log", append: true, null));
            }

            void CreateLogFile(string logFilePath, string log)
            {
                using (var appender = new FileAppender())
                {
                    appender.ConversionPattern = "{message}";
                    appender.Open(logFilePath, append: false, Encoding.UTF8);
                    appender.Trace(log);
                }
            }
        }

        public class Close
        {
            [Fact]
            public void OpenしてCloseする_成功する()
            {
                var appender = new FileAppender();

                appender.Open("test.log", append: false, Encoding.UTF8);
                var ex = Record.Exception(() => appender.Close());

                Assert.Null(ex);
            }

            [Fact]
            public void OpenしないでCloseする_許容する()
            {
                var appender = new FileAppender();

                var ex = Record.Exception(() => appender.Close());

                Assert.Null(ex);
            }

            [Fact]
            public void 連続してCloseする_許容する()
            {
                var appender = new FileAppender();

                appender.Open("test.log", append: false, Encoding.UTF8);
                appender.Close();
                var ex = Record.Exception(() => appender.Close());

                Assert.Null(ex);
            }
        }
    }
}

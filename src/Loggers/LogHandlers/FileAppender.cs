using SoftCube.Asserts;
using System;
using System.IO;
using System.Text;

namespace SoftCube.Loggers
{
    /// <summary>
    /// ファイルアペンダー。
    /// </summary>
    public class FileAppender : Appender
    {
        #region プロパティ

        /// <summary>
        /// ストリームライター。
        /// </summary>
        private StreamWriter Writer { get; }

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <param name="encoding">エンコーディング。</param>
        /// <param name="conversionPattern">変換パターン。</param>
        /// <param name="minLevel">最小レベル。</param>
        /// <param name="maxLevel">最大レベル。</param>
        public FileAppender(string filePath, Encoding encoding, string conversionPattern = "{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}", Level minLevel = Level.Trace, Level maxLevel = Level.Fatal)
            : base(conversionPattern, minLevel, maxLevel)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            Writer = new StreamWriter(filePath, append: true, encoding);
        }

        #endregion

        #region メソッド

        #region 破棄

        /// <summary>
        /// 破棄します。
        /// </summary>
        /// <param name="disposing">
        /// <see cref="IDisposable.Dispose"/> から呼び出されたかを示す値。
        /// <c>true</c> の場合、マネージリソースを破棄します。
        /// <c>false</c> の場合、マネージリソースを破棄しないでください。
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (Writer)
                {
                    Assert.True(Writer != null);
                    Writer.Close();
                    Writer.Dispose();
                }
            }
        }

        #endregion

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="log">ログ。</param>
        public override void Log(string log)
        {
            lock (Writer)
            {
                Writer.Write(log);
                Writer.Flush();
            }
        }

        #endregion
    }
}

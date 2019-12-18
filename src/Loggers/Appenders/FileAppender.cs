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
        public FileAppender(string filePath, Encoding encoding)
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

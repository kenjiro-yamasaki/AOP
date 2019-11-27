using System;
using System.IO;
using System.Text;

namespace SoftCube.Debugs
{
    /// <summary>
    /// ログファイルハンドラ。
    /// </summary>
    public class LogFileHandler
        : LogHandler
    {
        #region プロパティ

        /// <summary>
        /// ストリームライタ。
        /// </summary>
        private StreamWriter Writer { get; set; }

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="encoding">エンコーディング</param>
        public LogFileHandler(string filePath, Encoding encoding)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(filePath));
            Open(filePath, encoding);
        }

        #endregion

        #region メソッド

        #region IDisposable

        /// <summary>
        /// 破棄する。
        /// </summary>
        /// <param name="disposing">IDisposable.Dispose()から呼び出されたか</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        #endregion

        #region 開く

        /// <summary>
        /// ファイルを開く。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="encoding">エンコーディング</param>
        private void Open(string filePath, Encoding encoding)
        {
            Assert.IsTrue(Writer == null);

            Writer = new StreamWriter(filePath, append: true, encoding: encoding);
        }

        #endregion

        #region 閉じる

        /// <summary>
        /// ファイルを閉じる。
        /// </summary>
        private void Close()
        {
            Assert.IsTrue(Writer != null);

            Writer.Close();
            Writer.Dispose();
            Writer = null;
        }

        #endregion

        /// <summary>
        /// トレースログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public override void Trace(string message)
        {
            Assert.IsTrue(Writer != null);
            Assert.IsTrue(!string.IsNullOrEmpty(message));

            Writer.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} [TRACE]{message}");
            Writer.Flush();
        }

        /// <summary>
        /// 警告ログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public override void Warning(string message)
        {
            Assert.IsTrue(Writer != null);
            Assert.IsTrue(!string.IsNullOrEmpty(message));

            Writer.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} [WARNING]{message}");
            Writer.Flush();
        }

        /// <summary>
        /// エラーログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public override void Error(string message)
        {
            Assert.IsTrue(Writer != null);
            Assert.IsTrue(!string.IsNullOrEmpty(message));

            Writer.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} [ERROR]{message}");
            Writer.Flush();
        }

        /// <summary>
        /// デバッグログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public override void Debug(string message)
        {
            Assert.IsTrue(Writer != null);
            Assert.IsTrue(!string.IsNullOrEmpty(message));

            Writer.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} [DEBUG]{message}");
            Writer.Flush();
        }

        #endregion
    }
}

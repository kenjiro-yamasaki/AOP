using SoftCube.Asserts;
using System;
using System.IO;
using System.Text;

namespace SoftCube.Loggers
{
    /// <summary>
    /// ログファイルハンドラー。
    /// </summary>
    public class LogFileHandler : LogHandler
    {
        #region プロパティ

        /// <summary>
        /// ストリームライター。
        /// </summary>
        private StreamWriter Writer { get; set; }

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <param name="encoding">エンコーディング。</param>
        public LogFileHandler(string filePath, Encoding encoding)
        {
            Open(filePath, encoding);
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
                Close();
            }
        }

        #endregion

        #region 開く

        /// <summary>
        /// ログファイルを開きます。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <param name="encoding">エンコーディング。</param>
        private void Open(string filePath, Encoding encoding)
        {
            Assert.True(!string.IsNullOrEmpty(filePath));
            Assert.True(Writer == null);

            Writer = new StreamWriter(filePath, append: true, encoding: encoding);
        }

        #endregion

        #region 閉じる

        /// <summary>
        /// ログファイルを閉じます。
        /// </summary>
        private void Close()
        {
            Assert.True(Writer != null);

            Writer.Close();
            Writer.Dispose();
            Writer = null;
        }

        #endregion

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="log">ログ。</param>
        public override void Log(string log)
        {
            Writer.Write(log);
            Writer.Flush();
        }

        #endregion
    }
}

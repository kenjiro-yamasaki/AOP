using System;
using System.Text;

namespace SoftCube.Loggers
{
    /// <summary>
    /// ログ文字列ハンドラー。
    /// </summary>
    public class LogStringHandler : LogHandler
    {
        #region プロパティ

        /// <summary>
        /// 文字列ビルダー。
        /// </summary>
        private StringBuilder StringBuilder { get; } = new StringBuilder();

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public LogStringHandler()
        {
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
        }

        #endregion

        #region 変換

        /// <summary>
        /// 文字列に変換する。
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            return StringBuilder.ToString();
        }

        #endregion

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="log">ログ。</param>
        public override void Log(string log)
        {
            StringBuilder.Append(log);
        }

        #endregion
    }
}

using System;

namespace SoftCube.Debugs
{
    /// <summary>
    /// ログハンドラ。
    /// </summary>
    public abstract class LogHandler
        : IDisposable
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public LogHandler()
        {
        }

        #endregion

        #region メソッド

        #region IDisposable

        /// <summary>
        /// ファイナライザー。
        /// </summary>
        ~LogHandler()
        {
            Dispose(false);
        }

        /// <summary>
        /// 破棄する。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 破棄する。
        /// </summary>
        /// <param name="disposing">IDisposable.Dispose()から呼び出されたか</param>
        protected abstract void Dispose(bool disposing);

        #endregion

        /// <summary>
        /// トレースログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public abstract void Trace(string message);

        /// <summary>
        /// 警告ログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public abstract void Warning(string message);

        /// <summary>
        /// エラーログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public abstract void Error(string message);

        /// <summary>
        /// デバッグログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public abstract void Debug(string message);

        #endregion
    }
}

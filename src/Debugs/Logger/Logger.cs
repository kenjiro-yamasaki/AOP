using System.Collections.Generic;
using System.Diagnostics;

namespace SoftCube.Debugs
{
    /// <summary>
    /// ロガー。
    /// </summary>
    public static class Logger
    {
        #region プロパティ

        /// <summary>
        /// ログハンドラコレクション。
        /// </summary>
        private static List<LogHandler> Handlers { get; } = new List<LogHandler>();

        #endregion

        #region メソッド

        #region ログハンドラコレクション

        /// <summary>
        /// ログハンドラを追加する。
        /// </summary>
        /// <param name="handler">ログハンドラ</param>
        public static void Add(LogHandler handler)
        {
            Handlers.Add(handler);
        }

        /// <summary>
        /// ログハンドラをクリアし、破棄する。
        /// </summary>
        public static void ClearAndDisposeHandlers()
        {
            foreach (var handler in Handlers)
            {
                handler.Dispose();
            }

            Handlers.Clear();
        }

        #endregion

        /// <summary>
        /// トレースログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Trace(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Trace(message);
            }
        }

        /// <summary>
        /// 警告ログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Warning(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Warning(message);
            }
        }

        /// <summary>
        /// エラーログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Error(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Error(message);
            }
        }

        /// <summary>
        /// デバッグログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Debug(message);
            }
        }

        #endregion
    }
}

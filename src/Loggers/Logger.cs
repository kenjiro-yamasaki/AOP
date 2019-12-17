using System.Collections.Generic;

namespace SoftCube.Loggers
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
        private static IEnumerable<LogHandler> Handlers => handlers;
        private static readonly List<LogHandler> handlers = new List<LogHandler>();

        #endregion

        #region メソッド

        #region ログハンドラーコレクション

        /// <summary>
        /// ログハンドラーを追加します。
        /// </summary>
        /// <param name="handler">ログハンドラー。</param>
        public static void Add(LogHandler handler)
        {
            handlers.Add(handler);
        }

        /// <summary>
        /// ログハンドラーをクリアし、破棄します。
        /// </summary>
        public static void ClearAndDisposeHandlers()
        {
            foreach (var handler in Handlers)
            {
                handler.Dispose();
            }

            handlers.Clear();
        }

        #endregion

        /// <summary>
        /// トレース情報を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public static void Trace(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Trace(message);
            }
        }

        /// <summary>
        /// デバッグ情報を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public static void Debug(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Debug(message);
            }
        }

        /// <summary>
        /// 情報を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public static void Info(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Info(message);
            }
        }

        /// <summary>
        /// 警告を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public static void Warning(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Warning(message);
            }
        }

        /// <summary>
        /// エラーを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public static void Error(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Error(message);
            }
        }

        /// <summary>
        /// 致命的なエラーを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public static void Fatal(string message)
        {
            foreach (var handler in Handlers)
            {
                handler.Fatal(message);
            }
        }

        #endregion
    }
}

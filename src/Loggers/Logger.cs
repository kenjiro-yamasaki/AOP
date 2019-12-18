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
        /// アペンダーコレクション。
        /// </summary>
        private static IReadOnlyList<Appender> Appenders => appenders;
        private static readonly List<Appender> appenders = new List<Appender>();

        #endregion

        #region メソッド

        #region アペンダーコレクション

        /// <summary>
        /// アペンダーを追加します。
        /// </summary>
        /// <param name="appender">アペンダー。</param>
        public static void Add(Appender appender)
        {
            lock (appenders)
            {
                appenders.Add(appender);
            }
        }

        /// <summary>
        /// アペンダーをクリアし、破棄します。
        /// </summary>
        public static void ClearAndDisposeAppenders()
        {
            lock (appenders)
            {
                foreach (var appender in appenders)
                {
                    appender.Dispose();
                }

                appenders.Clear();
            }
        }

        #endregion

        /// <summary>
        /// トレースを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public static void Trace(string message)
        {
            lock (appenders)
            {
                foreach (var appender in appenders)
                {
                    appender.Trace(message);
                }
            }
        }

        /// <summary>
        /// デバッグを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public static void Debug(string message)
        {
            lock (appenders)
            {
                foreach (var appender in appenders)
                {
                    appender.Debug(message);
                }
            }
        }

        /// <summary>
        /// 情報を出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public static void Info(string message)
        {
            lock (appenders)
            {
                foreach (var appender in appenders)
                {
                    appender.Info(message);
                }
            }
        }

        /// <summary>
        /// 警告を出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public static void Warning(string message)
        {
            lock (appenders)
            {
                foreach (var appender in appenders)
                {
                    appender.Warning(message);
                }
            }
        }

        /// <summary>
        /// エラーを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public static void Error(string message)
        {
            lock (appenders)
            {
                foreach (var appender in appenders)
                {
                    appender.Error(message);
                }
            }
        }

        /// <summary>
        /// 致命的なエラーを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public static void Fatal(string message)
        {
            lock (appenders)
            {
                foreach (var appender in appenders)
                {
                    appender.Fatal(message);
                }
            }
        }

        #endregion
    }
}

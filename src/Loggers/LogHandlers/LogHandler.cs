using System;

namespace SoftCube.Loggers
{
    /// <summary>
    /// ログハンドラー。
    /// </summary>
    public abstract class LogHandler : IDisposable
    {
        #region プロパティ

        /// <summary>
        /// 最小ログレベル。
        /// 最小ログレベル以上のログのみを出力します。
        /// </summary>
        public LogLevel MinLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// 最大ログレベル。
        /// 最大ログレベル以下のログのみを出力します。
        /// </summary>
        public LogLevel MaxLevel { get; set; } = LogLevel.Fatal;

        /// <summary>
        /// 変換パターン。
        /// </summary>
        /// <remarks>
        /// 以下の変数を使用してでログ出力の変換パターンを指定します。
        /// ・date    : ログイベントが発生した時刻 (ローカルタイムゾーン)。
        /// ・level   : ログイベントのログレベル。
        /// ・message : ログイベントのログメッセージ。
        /// ・newline : 改行文字。
        /// </remarks>
        /// <example>
        /// 変換パターンを以下のように指定すると、
        /// "{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}"
        /// 以下のようなログが出力されます。
        /// "2019-12-17 20:51:29,565 [INFO ] - message\r\n"
        /// </example>
        public string ConversionPattern
        {
            get => conversionPattern;
            set
            {
                if (conversionPattern != value)
                {
                    conversionPattern = value;
                    formattedConversionPattern = new Lazy<string>(() => {
                        value = value.Replace("date",    "0");
                        value = value.Replace("level",   "1");
                        value = value.Replace("message", "2");
                        value = value.Replace("newline", "3");
                        return value;
                    });
                }
            }
        }
        private string conversionPattern;
        private Lazy<string> formattedConversionPattern = null;

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public LogHandler()
        {
            ConversionPattern = "{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}";
        }

        #endregion

        #region メソッド

        #region 破棄

        /// <summary>
        /// ファイナライザー。
        /// </summary>
        ~LogHandler()
        {
            Dispose(false);
        }

        /// <summary>
        /// 破棄します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 破棄します。
        /// </summary>
        /// <param name="disposing"><see cref="IDisposable.Dispose"/> から呼び出されたかを示す値。</param>
        protected abstract void Dispose(bool disposing);

        #endregion

        /// <summary>
        /// トレース情報を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public void Trace(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = LogLevel.Trace;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(level, message);
            }
        }

        /// <summary>
        /// デバッグ情報を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public void Debug(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = LogLevel.Debug;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(level, message);
            }
        }

        /// <summary>
        /// 情報を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public void Info(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = LogLevel.Info;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(level, message);
            }
        }

        /// <summary>
        /// 警告を出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public void Warning(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = LogLevel.Warning;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(level, message);
            }
        }

        /// <summary>
        /// エラーを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public void Error(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = LogLevel.Error;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(level, message);
            }
        }

        /// <summary>
        /// 致命的なエラーを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ。</param>
        public void Fatal(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = LogLevel.Fatal;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(level, message);
            }
        }

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="level">ログレベル。</param>
        /// <param name="message">ログメッセージ。</param>
        private void Log(LogLevel level, string message)
        {
            try
            {
                var log = string.Format(
                    formattedConversionPattern?.Value,
                    DateTime.Now,
                    level.ToDisplayName(),
                    message,
                    Environment.NewLine);

                Log(log);
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException($"ConversionPattern[{ConversionPattern}]が不正です。");
            }
        }

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="log">ログ。</param>
        public abstract void Log(string log);

        #endregion
    }
}

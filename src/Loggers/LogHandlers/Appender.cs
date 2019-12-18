using System;
using System.Diagnostics;
using System.Threading;

namespace SoftCube.Loggers
{
    /// <summary>
    /// アペンダー。
    /// </summary>
    public abstract class Appender : IDisposable
    {
        #region プロパティ

        /// <summary>
        /// 変換パターン。
        /// </summary>
        /// <remarks>
        /// 以下の変数を使用してログ出力の変換パターンを指定します。
        /// ・date    : ログイベントが発生した時刻 (ローカルタイムゾーン)。
        /// ・file    : ログイベントが発生したファイル名。
        /// ・level   : ログイベントのレベル。
        /// ・line    : ログイベントが発生したファイル行番号。
        /// ・message : ログイベントのメッセージ。
        /// ・method  : ログイベントが発生したメソッド名。
        /// ・newline : 改行文字。
        /// ・thread  : ログイベントが発生したスレッド番号。
        /// ・type    : ログイベントが発生した型名。
        /// </remarks>
        /// <example>
        /// 変換パターンは、以下の例のように指定します。
        /// ・"{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}" → "2019-12-17 20:51:29,565 [INFO ] - message\r\n"
        /// </example>
        public string ConversionPattern
        {
            get => conversionPattern;
            private set
            {
                if (conversionPattern != value)
                {
                    conversionPattern = value;
                    replacedConversionPattern = new Lazy<string>(() => {

                        // 置換が成功するように、長い文字列から置換します。
                        // たとえば、newlineより先にlineを置換すると正しく置換されません。
                        value = value.Replace("message", "0");
                        value = value.Replace("newline", "1");
                        value = value.Replace("method",  "2");
                        value = value.Replace("thread",  "3");
                        value = value.Replace("level",   "4");
                        value = value.Replace("date",    "5");
                        value = value.Replace("file",    "6");
                        value = value.Replace("line",    "7");
                        value = value.Replace("type",    "8");

                        return value;
                    });
                }
            }
        }
        private string conversionPattern;
        private Lazy<string> replacedConversionPattern = null;

        /// <summary>
        /// 最小レベル。
        /// </summary>
        /// <remarks>
        /// 最小レベル以上のログのみを出力します。
        /// </remarks>
        public Level MinLevel { get; }

        /// <summary>
        /// 最大レベル。
        /// </summary>
        /// <remarks>
        /// 最大レベル以下のログのみを出力します。
        /// </remarks>
        public Level MaxLevel { get; }

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="conversionPattern">変換パターン。</param>
        /// <param name="minLevel">最小レベル。</param>
        /// <param name="maxLevel">最大レベル。</param>
        public Appender(string conversionPattern = "{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}", Level minLevel = Level.Trace, Level maxLevel = Level.Fatal)
        {
            ConversionPattern = conversionPattern ?? throw new ArgumentNullException(nameof(conversionPattern));
            MinLevel = minLevel;
            MaxLevel = maxLevel;
        }

        #endregion

        #region メソッド

        #region 破棄

        /// <summary>
        /// ファイナライザー。
        /// </summary>
        ~Appender()
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
        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion

        /// <summary>
        /// トレースログを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void Trace(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = Level.Trace;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(Format(level, message));
            }
        }

        /// <summary>
        /// デバッグログを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void Debug(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = Level.Debug;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(Format(level, message));
            }
        }

        /// <summary>
        /// 情報ログを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void Info(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = Level.Info;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(Format(level, message));
            }
        }

        /// <summary>
        /// 警告ログを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void Warning(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = Level.Warning;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(Format(level, message));
            }
        }

        /// <summary>
        /// エラーログを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void Error(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = Level.Error;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(Format(level, message));
            }
        }

        /// <summary>
        /// 致命的なエラーログを出力します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void Fatal(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var level = Level.Fatal;
            if (MinLevel <= level && level <= MaxLevel)
            {
                Log(Format(level, message));
            }
        }

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="log">ログ。</param>
        public abstract void Log(string log);

        /// <summary>
        /// 変換パターンを使用して、ログをフォーマットします。
        /// </summary>
        /// <param name="level">ログレベル。</param>
        /// <param name="message">メッセージ。</param>
        /// <returns>ログ。</returns>
        private string Format(Level level, string message)
        {
            try
            {
                var stackFrame = new StackFrame(2, true);
                var type       = stackFrame.GetMethod().DeclaringType.FullName;
                var method     = stackFrame.GetMethod().Name;
                var file       = stackFrame.GetFileName();
                var line       = stackFrame.GetFileLineNumber();
                var date       = DateTime.Now;
                var newline    = Environment.NewLine;
                var thread     = Thread.CurrentThread.ManagedThreadId;

                return string.Format(
                    replacedConversionPattern?.Value,
                    message,
                    newline,
                    method,
                    thread,
                    level.ToDisplayName(),
                    date,
                    file,
                    line,
                    type);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException($"ConversionPattern[{ConversionPattern}]が不正です。");
            }
        }

        #endregion
    }
}

using System;
using System.Text;

namespace SoftCube.Loggers
{
    /// <summary>
    /// 文字列アペンダー。
    /// </summary>
    public class StringAppender : Appender
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
        /// <param name="conversionPattern">変換パターン。</param>
        /// <param name="minLevel">最小レベル。</param>
        /// <param name="maxLevel">最大レベル。</param>
        public StringAppender(string conversionPattern = "{date:yyyy-MM-dd HH:mm:ss,fff} [{level,-5}] - {message}{newline}", Level minLevel = Level.Trace, Level maxLevel = Level.Fatal)
            : base(conversionPattern, minLevel, maxLevel)
        {
        }

        #endregion

        #region メソッド

        #region 変換

        /// <summary>
        /// 文字列に変換する。
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            lock (StringBuilder)
            {
                return StringBuilder.ToString();
            }
        }

        #endregion

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="log">ログ。</param>
        public override void Log(string log)
        {
            lock (StringBuilder)
            {
                StringBuilder.Append(log);
            }
        }

        #endregion
    }
}

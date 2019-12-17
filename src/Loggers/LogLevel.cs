using System;

namespace SoftCube.Loggers
{
    /// <summary>
    /// ログレベル。
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// トレース情報。
        /// </summary>
        /// <remarks>
        /// デバッグ情報よりも、更に詳細な情報。
        /// </remarks>
        Trace = 0,

        /// <summary>
        /// デバッグ情報。
        /// </summary>
        /// <remarks>
        /// システムの動作状況に関する詳細な情報。
        /// </remarks>
        Debug = 1,

        /// <summary>
        /// 情報。
        /// </summary>
        /// <remarks>
        /// 実行時の何らかの注目すべき事象（開始や終了など）。
        /// メッセージ内容は簡潔に止めるべき。
        /// </remarks>
        Info = 2,

        /// <summary>
        /// 警告。
        /// </summary>
        /// <remarks>
        /// 廃止となったAPIの使用、APIの不適切な使用、エラーに近い事象など。
        /// 実行時に生じた異常とは言い切れないが正常とも異なる何らかの予期しない問題。
        /// </remarks>
        Warning = 3,

        /// <summary>
        /// エラー。
        /// </summary>
        /// <remarks>
        /// 予期しない実行時エラー。
        /// </remarks>
        Error = 4,

        /// <summary>
        /// 致命的なエラー。
        /// </summary>
        /// <remarks>
        /// プログラムの異常終了を伴うようなもの。
        /// </remarks>
        Fatal = 5,
    }

    /// <summary>
    /// <see cref="LogLevel"/> の拡張メソッド。
    /// </summary>
    public static class LogLevelExtensions
    {
        #region 静的メソッド

        /// <summary>
        /// ログレベルを表示名に変換する。
        /// </summary>
        /// <param name="logLevel">ログレベル</param>
        /// <returns>表示名</returns>
        public static string ToDisplayName(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "TRACE";

                case LogLevel.Debug:
                    return "DEBUG";

                case LogLevel.Info:
                    return "INFO";

                case LogLevel.Warning:
                    return "WARNING";

                case LogLevel.Error:
                    return "ERROR";

                case LogLevel.Fatal:
                    return "FATAL";

                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }
}

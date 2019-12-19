using SoftCube.Runtime;
using System.Text;

namespace SoftCube.Logger
{
    /// <summary>
    /// ローリングファイルアペンダー。
    /// </summary>
    /// <remarks>
    /// <see cref="RollingFileAppender"/> は <see cref="FileAppender"/> クラスを拡張したクラスです。
    /// ログファイルが一定のサイズを超えたとき、バックアップファイルを作成したい場合に使用します。
    /// </remarks>
    public class RollingFileAppender : FileAppender
    {
        #region プロパティ

        /// <summary>
        /// 最大ファイルサイズ（単位：byte）。
        /// </summary>
        /// <remarks>
        /// ローテンションするログファイルサイズを指定します。
        /// </remarks>
        public int MaxFileSize { get; set; }

        /// <summary>
        /// 最大バックアップ数。
        /// </summary>
        /// <remarks>
        /// バックアップファイルをいくつ保持するか指定します。
        /// 例えば、<see cref="MaxBackupCount"/>=3 を指定すると、ログファイル.1→ログファイル.2→ログファイル.3とローテンションしていき、それ以上古くなると破棄されます。
        /// </remarks>
        public int MaxBackupCount { get; set; }

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public RollingFileAppender()
        {
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="systemClock">システムクロック。</param>
        public RollingFileAppender(ISystemClock systemClock)
            : base(systemClock)
        {
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <param name="append">ファイルにログを追加するかを示す値。</param>
        /// <param name="encoding">エンコーディング。</param>
        /// <seealso cref="Open(string, bool, Encoding)"/>
        public RollingFileAppender(string filePath, bool append, Encoding encoding)
            : base(filePath, append, encoding)
        {
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="systemClock">システムクロック。</param>
        /// <param name="filePath">ファイルパス。</param>
        /// <param name="append">ファイルにログを追加するかを示す値。</param>
        /// <param name="encoding">エンコーディング。</param>
        /// <seealso cref="Open(string, bool, Encoding)"/>
        public RollingFileAppender(ISystemClock systemClock, string filePath, bool append, Encoding encoding)
            : base(systemClock, filePath, append, encoding)
        {
        }

        #endregion

        #region メソッド

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="log">ログ。</param>
        public override void Log(string log)
        {
            base.Log(log);



        }

        #endregion
    }
}

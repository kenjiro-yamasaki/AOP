using SoftCube.Runtime;
using System.IO;
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
        public long MaxFileSize { get; set; }

        /// <summary>
        /// 最大バックアップ数。
        /// </summary>
        /// <remarks>
        /// バックアップファイルをいくつ保持するか指定します。
        /// 例えば、<see cref="MaxBackupCount"/>=3 を指定すると、
        /// ログファイル.1→ログファイル.2→ログファイル.3とローテンションしていき、
        /// それ以上古くなると破棄されます。
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

            if (MaxFileSize <= FileSize)
            {
                RollLogAndBackupFiles();
            }
        }

        /// <summary>
        /// ログファイル (とバックアップファイル) をローテンションします。
        /// </summary>
        private void RollLogAndBackupFiles()
        {
            var filePath      = FilePath;
            var directoryName = Path.GetDirectoryName(filePath);
            var fileName      = Path.GetFileName(filePath);
            var baseName      = Path.GetFileNameWithoutExtension(fileName);
            var extension     = Path.GetExtension(fileName);
            var encoding      = Encoding;

            // 現在のログファイルを閉じます。
            Close();

            // 既に存在するバックアップファイルをローテーションします。
            for (int backupIndex = MaxBackupCount - 1; 1 <= backupIndex; backupIndex--)
            {
                var srcFilePath  = Path.Combine(directoryName, $"{baseName}{backupIndex}.{extension}");
                var destFilePath = Path.Combine(directoryName, $"{baseName}{backupIndex}.{extension}");

                if (!File.Exists(srcFilePath))
                {
                    continue;
                }

                if (File.Exists(destFilePath))
                {
                    File.Delete(destFilePath);
                }

                File.Move(srcFilePath, destFilePath);
            }

            // 現在のログファイルを新規バックアップファイルとします。
            {
                var logFilePath    = filePath;
                var backupIndex    = 1;
                var backupFilePath = Path.Combine(directoryName, $"{baseName}{backupIndex}.{extension}");

                File.Move(logFilePath, backupFilePath);
            }

            // ログファイルを新規作成します。
            Open(filePath, append: false, encoding);
        }

        #endregion
    }
}

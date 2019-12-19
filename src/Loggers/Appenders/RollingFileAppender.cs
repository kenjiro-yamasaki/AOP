using System.IO;

namespace SoftCube.Loggers
{
    /// <summary>
    /// 
    /// </summary>
    public class RollingFileAppender : Appender
    {
        #region プロパティ

        /// <summary>
        /// ファイルアペンダー。
        /// </summary>
        private FileAppender FileAppender { get; set; }

        #endregion





        #region メソッド

        public override void Log(string log)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}

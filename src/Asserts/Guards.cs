using System;

namespace SoftCube.Asserts
{
    /// <summary>
    /// アサート。
    /// </summary>
    public partial class Assert
    {
        #region 静的メソッド

        /// <summary>
        /// 引数が非nullであることを保証する。
        /// </summary>
        /// <param name="argName">引数の名前</param>
        /// <param name="argValue">引数の値</param>
        private static void GuardArgumentNotNull(string argName, object argValue)
        {
            if (argValue == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        #endregion
    }
}

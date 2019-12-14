using System.Diagnostics;

namespace SoftCube.Asserts
{
    /// <summary>
    /// アサート。
    /// </summary>
    public static partial class Assert
    {
        #region 静的メソッド

        /// <summary>
        /// オブジェクト参照が非nullであることを検証する。
        /// </summary>
        /// <param name="object">オブジェクト</param>
        /// <exception cref="NotNullException">オブジェクト参照がnullの場合、投げられる</exception>
        public static void NotNull(object @object)
        {
            if (@object == null)
            {
                throw new NotNullException();
            }
        }

        /// <summary>
        /// オブジェクト参照がnullであることを検証する。
        /// </summary>
        /// <param name="object">オブジェクト</param>
        /// <exception cref="NullException">オブジェクト参照が非nullの場合、投げられる</exception>
        public static void Null(object @object)
        {
            if (@object != null)
            {
                throw new NullException(@object);
            }
        }

        #endregion
    }
}

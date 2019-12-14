namespace SoftCube.Asserts
{
    /// <summary>
    /// アサート。
    /// </summary>
    public static partial class Assert
    {
        #region 静的メソッド

        /// <summary>
        /// オブジェクトが同じインスタンスではないことを検証する。
        /// </summary>
        /// <param name="expected">期待値。</param>
        /// <param name="actual">実測値。</param>
        /// <exception cref="NotSameException">オブジェクトが同じインスタンスである場合、投げられる。</exception>
        public static void NotSame(object expected, object actual)
        {
            if (object.ReferenceEquals(expected, actual))
            {
                throw new NotSameException();
            }
        }

        /// <summary>
        /// オブジェクトが同じインスタンスではあることを検証する。
        /// </summary>
        /// <param name="expected">期待値。</param>
        /// <param name="actual">実測値。</param>
        /// <exception cref="SameException">オブジェクトが同じインスタンスではない場合、投げられる。</exception>
        public static void Same(object expected, object actual)
        {
            if (!object.ReferenceEquals(expected, actual))
            {
                throw new SameException(expected, actual);
            }
        }

        #endregion
    }
}

namespace SoftCube.Asserts
{
    /// <summary>
    /// NotEqualアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.NotEqual(...)の失敗時に投げられる。
    /// </remarks>
    public class NotEqualException : AssertExpectedActualException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="expected">期待値</param>
        /// <param name="actual">実測値</param>
        public NotEqualException(string expected, string actual)
            : base("Not " + expected, actual, "Assert.NotEqual() Failure")
        {
        }

        #endregion
    }
}

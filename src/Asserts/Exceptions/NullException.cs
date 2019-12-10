namespace SoftCube.Asserts
{
    /// <summary>
    /// nullアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.Null(...)の失敗時に投げられる。
    /// </remarks>
    public class NullException : AssertExpectedActualException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="actual">実測値</param>
        public NullException(object actual)
            : base(null, actual, "Assert.Null() Failure")
        {
        }

        #endregion
    }
}
namespace SoftCube.Asserts
{
    /// <summary>
    /// 非nullアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.NotNull(...)の失敗時に投げられる。
    /// </remarks>
    public class NotNullException : AssertException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public NotNullException()
            : base("Assert.NotNull() Failure")
        {
        }

        #endregion
    }
}
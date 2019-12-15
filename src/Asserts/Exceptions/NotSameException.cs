namespace SoftCube.Asserts
{
    /// <summary>
    /// NotSameアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.NotSame(...)の失敗時に投げられます。
    /// </remarks>
    public class NotSameException : AssertException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public NotSameException()
            : base("Assert.NotSame() Failure")
        {
        }

        #endregion
    }
}

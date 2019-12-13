namespace SoftCube.Asserts
{
    /// <summary>
    /// Falseアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.False(...)の失敗時に投げられる。
    /// </remarks>
    public class FalseException : AssertExpectedActualException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="acutual">実測値</param>
        /// <param name="message">メッセージ</param>
        public FalseException(bool? acutual, string message)
            : base("False", acutual == null ? "(null)" : acutual.ToString(), message ?? "Assert.False() Failure")
        {
        }

        #endregion
    }
}
namespace SoftCube.Asserts
{
    /// <summary>
    /// Falseアサート例外。
    /// </summary>
    public class FalseException : AssertExpectedActualException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="value">実測値</param>
        public FalseException(string message, bool? value)
            : base("False", value == null ? "(null)" : value.ToString(), message ?? "Assert.False() Failure")
        {
        }

        #endregion
    }
}
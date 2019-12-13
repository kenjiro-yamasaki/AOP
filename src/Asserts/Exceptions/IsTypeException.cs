namespace SoftCube.Asserts
{
    /// <summary>
    /// IsTypeアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.IsType(...)の失敗時に投げられる。
    /// 上記のアサートは、期待値と実測値の型が正確に一致することを検証する。
    /// </remarks>
    public class IsTypeException : AssertExpectedActualException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="expectedTypeName">期待値（型名）</param>
        /// <param name="actualTypeName">実測値（型名）</param>
        public IsTypeException(string expectedTypeName, string actualTypeName)
            : base(expectedTypeName, actualTypeName, "Assert.IsType() Failure")
        {
        }

        #endregion
    }
}
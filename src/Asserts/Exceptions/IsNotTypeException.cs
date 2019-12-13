using System;

namespace SoftCube.Asserts
{
    /// <summary>
    /// IsNotTypeアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.IsNotType(...)の失敗時に投げられる。
    /// 上記のアサートは、期待値と実測値の型が正確に一致しないことを検証する。
    /// </remarks>
    public class IsNotTypeException : AssertExpectedActualException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="expectedTypeName">期待値（型）</param>
        /// <param name="actualTypeName">実測値</param>
        public IsNotTypeException(Type expected, object actual)
            : base(expected, actual == null ? null : actual.GetType(), "Assert.IsNotType() Failure")
        {
        }

        #endregion
    }
}

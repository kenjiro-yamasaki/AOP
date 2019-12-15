using System;
using System.Collections;

namespace SoftCube.Asserts
{
    /// <summary>
    /// Emptyアサート例外。
    /// </summary>
    /// <remarks>
    /// 本例外は、Assert.Empty(...)の失敗時に投げられます。
    /// </remarks>
    public class EmptyException : AssertExpectedActualException
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="collection">検証に失敗したコレクション</param>
        public EmptyException(IEnumerable collection)
            : base("<empty>", ArgumentFormatter.Format(collection), "Assert.Empty() Failure")
        {
        }

        #endregion
    }
}

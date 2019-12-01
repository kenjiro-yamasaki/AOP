using System;

namespace SoftCube.Asserts
{
    /// <summary>
    /// アサート例外。
    /// </summary>
    public class AssertException : Exception
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public AssertException()
        {
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public AssertException(string message)
            : this(message, (Exception)null)
        {
        }

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="innerException">内部例外</param>
        protected AssertException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
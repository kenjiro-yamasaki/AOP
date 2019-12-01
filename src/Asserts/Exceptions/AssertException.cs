using System;

namespace SoftCube.Asserts
{
    /// <summary>
    /// �A�T�[�g��O�B
    /// </summary>
    public class AssertException : Exception
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        public AssertException()
        {
        }

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="message">���b�Z�[�W</param>
        public AssertException(string message)
            : this(message, (Exception)null)
        {
        }

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="message">���b�Z�[�W</param>
        /// <param name="innerException">������O</param>
        protected AssertException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
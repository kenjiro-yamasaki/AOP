namespace SoftCube.Asserts
{
    /// <summary>
    /// False�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.False(...)�̎��s���ɓ������܂��B
    /// </remarks>
    public class FalseException : AssertExpectedActualException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="acutual">�����l�B</param>
        /// <param name="message">���b�Z�[�W</param>
        public FalseException(bool? acutual, string message)
            : base("False", acutual == null ? "(null)" : acutual.ToString(), message ?? "Assert.False() Failure")
        {
        }

        #endregion
    }
}
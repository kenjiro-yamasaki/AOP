namespace SoftCube.Asserts
{
    /// <summary>
    /// true�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.True(...)�̎��s���ɓ������܂��B
    /// </remarks>
    public class TrueException : AssertExpectedActualException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="acutual">�����l�B</param>
        /// <param name="message">���b�Z�[�W</param>
        public TrueException(bool? acutual, string message)
            : base("True", acutual == null ? "(null)" : acutual.ToString(), message ?? "Assert.True() Failure")
        {
        }

        #endregion
    }
}
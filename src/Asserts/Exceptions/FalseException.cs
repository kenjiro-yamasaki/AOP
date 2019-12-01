namespace SoftCube.Asserts
{
    /// <summary>
    /// False�A�T�[�g��O�B
    /// </summary>
    public class FalseException : AssertExpectedActualException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="message">���b�Z�[�W</param>
        /// <param name="value">�����l</param>
        public FalseException(string message, bool? value)
            : base("False", value == null ? "(null)" : value.ToString(), message ?? "Assert.False() Failure")
        {
        }

        #endregion
    }
}
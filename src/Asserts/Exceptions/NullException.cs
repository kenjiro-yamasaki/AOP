namespace SoftCube.Asserts
{
    /// <summary>
    /// null�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.Null(...)�̎��s���ɓ�������B
    /// </remarks>
    public class NullException : AssertExpectedActualException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="actual">�����l</param>
        public NullException(object actual)
            : base(null, actual, "Assert.Null() Failure")
        {
        }

        #endregion
    }
}
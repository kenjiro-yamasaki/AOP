namespace SoftCube.Asserts
{
    /// <summary>
    /// NotSame�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.NotSame(...)�̎��s���ɓ������܂��B
    /// </remarks>
    public class NotSameException : AssertException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        public NotSameException()
            : base("Assert.NotSame() Failure")
        {
        }

        #endregion
    }
}

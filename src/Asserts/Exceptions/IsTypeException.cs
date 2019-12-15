namespace SoftCube.Asserts
{
    /// <summary>
    /// IsType�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.IsType(...)�̎��s���ɓ������܂��B
    /// ��L�̃A�T�[�g�́A���Ғl�Ǝ����l�̌^�����m�Ɉ�v���邱�Ƃ����؂��܂��B
    /// </remarks>
    public class IsTypeException : AssertExpectedActualException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="expectedTypeName">���Ғl(�^��)</param>
        /// <param name="actualTypeName">�����l(�^��)</param>
        public IsTypeException(string expectedTypeName, string actualTypeName)
            : base(expectedTypeName, actualTypeName, "Assert.IsType() Failure")
        {
        }

        #endregion
    }
}
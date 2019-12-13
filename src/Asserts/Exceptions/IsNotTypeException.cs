using System;

namespace SoftCube.Asserts
{
    /// <summary>
    /// IsNotType�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.IsNotType(...)�̎��s���ɓ�������B
    /// ��L�̃A�T�[�g�́A���Ғl�Ǝ����l�̌^�����m�Ɉ�v���Ȃ����Ƃ����؂���B
    /// </remarks>
    public class IsNotTypeException : AssertExpectedActualException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="expectedTypeName">���Ғl�i�^�j</param>
        /// <param name="actualTypeName">�����l</param>
        public IsNotTypeException(Type expected, object actual)
            : base(expected, actual == null ? null : actual.GetType(), "Assert.IsNotType() Failure")
        {
        }

        #endregion
    }
}

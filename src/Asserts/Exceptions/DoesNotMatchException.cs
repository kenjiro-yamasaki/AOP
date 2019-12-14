using System;
using System.Globalization;

namespace SoftCube.Asserts
{
    /// <summary>
    /// DoesNotMatch�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.DoesNotMatch(...)�̎��s���ɓ�������B
    /// </remarks>
    public class DoesNotMatchException : AssertException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="expectedRegexPattern">���Ғl(���K�\��)</param>
        /// <param name="actual">�����l</param>
        public DoesNotMatchException(object expectedRegexPattern, object actual)
            : base(string.Format(CultureInfo.CurrentCulture, "Assert.DoesNotMatch() Failure:{2}Regex: {0}{2}Value: {1}", expectedRegexPattern, actual, Environment.NewLine))
        {
        }

        #endregion
    }
}
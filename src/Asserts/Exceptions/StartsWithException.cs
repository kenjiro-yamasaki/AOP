using System;
using System.Globalization;

namespace SoftCube.Asserts
{
    /// <summary>
    /// StartsWith�A�T�[�g��O�B
    /// </summary>
    /// <remarks>
    /// �{��O�́AAssert.StartsWith(...)�̎��s���ɓ�������B
    /// </remarks>
    public class StartsWithException : AssertException
    {
        #region �R���X�g���N�^�[

        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="expected">���Ғl</param>
        /// <param name="actual">�����l</param>
        public StartsWithException(string expected, string actual)
            : base(string.Format(CultureInfo.CurrentCulture, "Assert.StartsWith() Failure:{2}Expected: {0}{2}Actual:   {1}", expected ?? "(null)", ShortenActual(expected, actual) ?? "(null)", Environment.NewLine))
        {
        }

        #endregion

        #region �ÓI���\�b�h

        /// <summary>
        /// �����l���ȗ�����B
        /// </summary>
        /// <param name="expected">���Ғl</param>
        /// <param name="actual">�����l</param>
        /// <returns>�ȗ����ꂽ�����l</returns>
        private static string ShortenActual(string expected, string actual)
        {
            if (expected == null || actual == null || actual.Length <= expected.Length)
            {
                return actual;
            }

            return actual.Substring(0, expected.Length) + "...";
        }

        #endregion
    }
}
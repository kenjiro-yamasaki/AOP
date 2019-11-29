using System;
using System.Diagnostics;

namespace SoftCube.Debugs
{
    /// <summary>
    /// アサート。
    /// </summary>
    public static class Assert
    {
        #region メソッド

        /// <summary>
        /// 参照オブジェクトがnullであることを表明する。
        /// </summary>
        /// <param name="object">参照オブジェクト</param>
        [Conditional("DEBUG")]
        public static void IsNull(object @object)
        {
            if (@object != null)
            {
                throw new ArgumentException($"アサートに失敗しました\n{new StackTrace()}");
            }
        }

        /// <summary>
        /// 参照オブジェクトがnullでないことを表明する。
        /// </summary>
        /// <param name="object">参照オブジェクト</param>
        [Conditional("DEBUG")]
        public static void IsNotNull(object @object)
        {
            if (@object == null)
            {
                throw new ArgumentException($"アサートに失敗しました\n{new StackTrace()}");
            }
        }

        /// <summary>
        /// 条件が真であることを表明する。
        /// </summary>
        /// <param name="condition">条件</param>
        [Conditional("DEBUG")]
        public static void IsTrue(bool condition)
        {
            if (!condition)
            {
                throw new ArgumentException($"アサートに失敗しました\n{new StackTrace()}");
            }
        }

        /// <summary>
        /// 条件が偽であることを表明する。
        /// </summary>
        /// <param name="condition">条件</param>
        [Conditional("DEBUG")]
        public static void IsFalse(bool condition)
        {
            if (condition)
            {
                throw new ArgumentException($"アサートに失敗しました\n{new StackTrace()}");
            }
        }

        /// <summary>
        /// 失敗を表明する。
        /// </summary>
        [Conditional("DEBUG")]
        public static void Fail()
        {
            throw new ArgumentException($"アサートに失敗しました\n{new StackTrace()}");
        }

        #endregion
    }
}

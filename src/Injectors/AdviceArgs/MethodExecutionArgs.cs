﻿using System;
using System.Reflection;

namespace SoftCube.Injectors
{
    /// <summary>
    /// メソッド実行引数。
    /// </summary>
    public class MethodExecutionArgs : AdviceArgs
    {
        #region プロパティ

        /// <summary>
        /// メソッド情報。
        /// </summary>
        public MethodBase Method { get; set; }

        /// <summary>
        /// 引数コレクション。
        /// </summary>
        public Arguments Arguments { get; }

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="instance">インスタンス</param>
        /// <param name="arguments">引数コレクション</param>
        public MethodExecutionArgs(object instance, Arguments arguments)
            : base(instance)
        {
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        #endregion
    }
}

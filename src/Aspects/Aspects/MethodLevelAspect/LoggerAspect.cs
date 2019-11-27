using System;

namespace SoftCube.Aspects
{
    /// <summary>
    /// ロガーアスペクト。
    /// </summary>
    public class LoggerAspect : OnMethodBoundaryAspect
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public LoggerAspect()
        {
        }

        #endregion

        #region メソッド

        /// <summary>
        /// メッソドの開始イベントハンドラー。
        /// </summary>
        /// <param name="args">メソッド実行引数</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("OnEntry");
        }

        /// <summary>
        /// メッソドの正常終了イベントハンドラー。
        /// </summary>
        /// <param name="args">メソッド実行引数</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("OnSuccess");
        }

        /// <summary>
        /// メッソドの例外終了イベントハンドラー。
        /// </summary>
        /// <param name="args">メソッド実行引数</param>
        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine("OnException");
        }

        /// <summary>
        /// メッソドの終了イベントハンドラー。
        /// </summary>
        /// <param name="args">メソッド実行引数</param>
        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("OnExit");
        }

        #endregion
    }
}

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
        /// メソッド開始イベントハンドラー。
        /// </summary>
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("OnEntry");
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("OnSuccess");
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine("OnException");
        }

        /// <summary>
        /// メソッド終了イベントハンドラー。
        /// </summary>
        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("OnExit");
        }

        #endregion
    }
}

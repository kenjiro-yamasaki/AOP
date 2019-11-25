using System;

namespace SoftCube.Injectors
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
        public override void OnEntry()
        {
            Console.WriteLine("OnEntry");
        }

        /// <summary>
        /// メソッド終了イベントハンドラー。
        /// </summary>
        public override void OnExit()
        {
            Console.WriteLine("OnExit");
        }

        #endregion
    }
}

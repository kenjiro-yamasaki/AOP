using Mono.Cecil;
using System;

namespace SoftCube.Injectors
{
    /// <summary>
    /// メソッド注入属性。
    /// </summary>
    public abstract class MethodInjectorAttribute : Attribute
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MethodInjectorAttribute()
            : base()
        {
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 注入する。
        /// </summary>
        /// <param name="method">注入対象のメソッド定義</param>
        public void Inject(MethodDefinition method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            OnInject(method);
        }

        /// <summary>
        /// 注入する。
        /// </summary>
        /// <param name="target">注入対象のメソッド定義</param>
        protected abstract void OnInject(MethodDefinition method);

        #endregion
    }
}

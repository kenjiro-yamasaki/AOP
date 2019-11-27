using Mono.Cecil;
using System;

namespace SoftCube.Aspects
{
    /// <summary>
    /// パラメーター注入属性。
    /// </summary>
    public abstract class ParameterLevelAspect : Attribute
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public ParameterLevelAspect()
            : base()
        {
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 注入する。
        /// </summary>
        /// <param name="target">注入対象のパラメーター定義</param>
        public void Inject(ParameterDefinition target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            OnInject(target);
        }

        /// <summary>
        /// 注入する。
        /// </summary>
        /// <param name="target">注入対象のパラメーター定義</param>
        protected abstract void OnInject(ParameterDefinition target);

        #endregion
    }
}

using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Linq;

namespace SoftCube.Aspects
{
    /// <summary>
    /// 非nullパラメーターアスペクト。
    /// </summary>
    /// <remarks>
    /// この属性を付けたパラメーターが、nullを許容しないことを明示する。
    /// nullを渡された場合、System.ArgumentNullExceptionを投げる。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    [Serializable]
    public class NotNullAspect : ParameterLevelAspect
    {
        #region メソッド

        /// <summary>
        /// 注入する。
        /// </summary>
        /// <param name="target">注入対象のパラメーター定義</param>
        protected override void OnInject(ParameterDefinition target)
        {
            if (!target.ParameterType.IsValueType)
            {
                var method = (target.Method as MethodDefinition);
                var argumentNullExceptionCtor = method.DeclaringType.Module.Assembly.MainModule.ImportReference(typeof(ArgumentNullException).GetConstructor(new Type[] { typeof(string) }));
                var processor = method.Body.GetILProcessor();
                var first = processor.Body.Instructions[0];

                processor.InsertBefore(first, processor.Create(OpCodes.Ldarg, target));
                processor.InsertBefore(first, processor.Create(OpCodes.Brtrue_S, first));
                processor.InsertBefore(first, processor.Create(OpCodes.Ldstr, target.Name));
                processor.InsertBefore(first, processor.Create(OpCodes.Newobj, argumentNullExceptionCtor));
                processor.InsertBefore(first, processor.Create(OpCodes.Throw));
            }
        }

        #endregion
    }
}

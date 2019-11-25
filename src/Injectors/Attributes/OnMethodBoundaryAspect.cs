using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SoftCube.Injectors
{
    /// <summary>
    /// メソッド境界アスペクト属性。
    /// </summary>
    public abstract class OnMethodBoundaryAspect : MethodInjectorAttribute
    {
        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public OnMethodBoundaryAspect()
        {
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 注入する。
        /// </summary>
        /// <param name="target">注入対象のメソッド定義</param>
        protected override void OnInject(MethodDefinition method)
        {
            var processor = method.Body.GetILProcessor();
            foreach (var instruction in processor.Body.Instructions)
            {
                var opCode = instruction.OpCode;
                var operand = instruction.Operand;

                Debug.WriteLine($"{opCode}, {operand}");
            }

            var module = method.DeclaringType.Module.Assembly.MainModule;

            // ローカル変数を追加する。
            var aspectIndex = processor.Body.Variables.Count();
            processor.Body.Variables.Add(new VariableDefinition(module.ImportReference(GetType())));

            var methodExecutionArgsIndex = processor.Body.Variables.Count();
            processor.Body.Variables.Add(new VariableDefinition(module.ImportReference(typeof(MethodExecutionArgs))));

            // 命令を書き換える。
            {
                var first = processor.Body.Instructions.First();

                // OnMethodBoundaryAspectの派生クラスを生成する。
                processor.InsertBefore(first, processor.Create(OpCodes.Newobj, module.ImportReference(GetType().GetConstructor(new Type[] { }))));
                processor.InsertBefore(first, processor.Create(OpCodes.Stloc, aspectIndex));

                // MethodExecutionArgsを生成する。
                processor.InsertBefore(first, processor.Create(OpCodes.Ldarg_0));
                processor.InsertBefore(first, processor.Create(OpCodes.Ldc_I4, method.Parameters.Count));
                processor.InsertBefore(first, processor.Create(OpCodes.Newarr, module.ImportReference(typeof(object))));
                for (int parameterIndex = 0; parameterIndex < method.Parameters.Count; parameterIndex++)
                {
                    var parameter = method.Parameters[parameterIndex];
                    processor.InsertBefore(first, processor.Create(OpCodes.Dup));
                    processor.InsertBefore(first, processor.Create(OpCodes.Ldc_I4, parameterIndex));
                    processor.InsertBefore(first, processor.Create(OpCodes.Ldarg, parameterIndex + 1));
                    if (parameter.ParameterType.IsValueType)
                    {
                        processor.InsertBefore(first, processor.Create(OpCodes.Box, parameter.ParameterType));
                    }
                    processor.InsertBefore(first, processor.Create(OpCodes.Stelem_Ref));
                }
                processor.InsertBefore(first, processor.Create(OpCodes.Newobj, module.ImportReference(typeof(Arguments).GetConstructor(new Type[] { typeof(object[]) }))));
                processor.InsertBefore(first, processor.Create(OpCodes.Newobj, module.ImportReference(typeof(MethodExecutionArgs).GetConstructor(new Type[] { typeof(object), typeof(Arguments) }))));
                processor.InsertBefore(first, processor.Create(OpCodes.Stloc, methodExecutionArgsIndex));

                processor.InsertBefore(first, processor.Create(OpCodes.Ldloc, methodExecutionArgsIndex));
                processor.InsertBefore(first, processor.Create(OpCodes.Call, module.ImportReference(typeof(MethodBase).GetMethod(nameof(MethodBase.GetCurrentMethod), new Type[]{ }))));
                processor.InsertBefore(first, processor.Create(OpCodes.Callvirt, module.ImportReference(typeof(MethodExecutionArgs).GetProperty(nameof(MethodExecutionArgs.Method)).GetSetMethod())));

                // OnEntryメソッドを呼び出す。
                processor.InsertBefore(first, processor.Create(OpCodes.Ldloc, aspectIndex));
                processor.InsertBefore(first, processor.Create(OpCodes.Ldloc, methodExecutionArgsIndex));
                processor.InsertBefore(first, processor.Create(OpCodes.Callvirt, module.ImportReference(GetType().GetMethod(nameof(OnEntry)))));
            }

            {
                var last = processor.Body.Instructions.Last();

                // OnExitメソッドを呼び出す。
                processor.InsertBefore(last, processor.Create(OpCodes.Ldloc, aspectIndex));
                processor.InsertBefore(last, processor.Create(OpCodes.Ldloc, methodExecutionArgsIndex));
                processor.InsertBefore(last, processor.Create(OpCodes.Callvirt, module.ImportReference(GetType().GetMethod(nameof(OnExit)))));
            }
        }

        #region イベントハンドラー

        /// <summary>
        /// メソッド開始イベントハンドラー。
        /// </summary>
        public virtual void OnEntry(MethodExecutionArgs args)
        {
        }

        /// <summary>
        /// メソッド終了イベントハンドラー。
        /// </summary>
        public virtual void OnExit(MethodExecutionArgs args)
        {
        }

        #endregion

        #endregion
    }
}

using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Diagnostics;
using System.Linq;

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

            var type        = this.GetType();
            var constructor = module.ImportReference(type.GetConstructor(new Type[] { }));
            var onEntry     = module.ImportReference(type.GetMethod(nameof(OnEntry)));
            var onExit      = module.ImportReference(type.GetMethod(nameof(OnExit)));

            // 属性のローカル変数を追加する。
            var index = processor.Body.Variables.Count();
            processor.Body.Variables.Add(new VariableDefinition(module.ImportReference(type)));
            processor.Body.MaxStackSize += 1;

            var first = processor.Body.Instructions.First();
            processor.InsertBefore(first, processor.Create(OpCodes.Newobj, constructor));
            processor.InsertBefore(first, processor.Create(OpCodes.Stloc, index));
            processor.InsertBefore(first, processor.Create(OpCodes.Ldloc, index));
            processor.InsertBefore(first, processor.Create(OpCodes.Callvirt, onEntry));

            var last  = processor.Body.Instructions.Last();
            //processor.InsertBefore(last, processor.Create(OpCodes.Leave_S, last));
            processor.InsertBefore(last, processor.Create(OpCodes.Ldloc, index));
            processor.InsertBefore(last, processor.Create(OpCodes.Callvirt, onExit));
            //processor.InsertBefore(last, processor.Create(OpCodes.Endfinally));
        }

        #region イベントハンドラー

        /// <summary>
        /// メソッド開始イベントハンドラー。
        /// </summary>
        public virtual void OnEntry()
        {
        }

        /// <summary>
        /// メソッド終了イベントハンドラー。
        /// </summary>
        public virtual void OnExit()
        {
        }

        #endregion

        #endregion
    }
}

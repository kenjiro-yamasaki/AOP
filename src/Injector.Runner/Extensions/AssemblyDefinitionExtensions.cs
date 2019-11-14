using Mono.Cecil;
using System.Linq;

namespace SoftCube.Injector.Runner
{
    /// <summary>
    /// AssemblyDefinitionの拡張メソッド。
    /// </summary>
    internal static class AssemblyDefinitionExtensions
    {
        #region 静的メソッド

        internal static void Inject(this AssemblyDefinition @this)
        {
            //var baseDirectory = @"C:\Users\kenji\Projects\Kernel\src\Injector.Sample\bin\x64\Debug\net4.7.2";

            //// 対象となる型、メソッド名
            //var targetTypeName = "SoftCube.Aspect.Sample.Program";
            //var targetMethodName = "Main";

            //// 差し込みたいアセンブリと型、メソッド名
            //var injectAssemblyName = "SoftCube.Aspect.dll";
            //var injectAssemblyPath = Path.Combine(baseDirectory, injectAssemblyName);
            //var injectTypeName = "SoftCube.Aspect.AspectInitialize";
            //var injectMethodName = "Initialize";


            //var targetType = @this.MainModule.GetType(targetTypeName);
            //var targetMethod = targetType.Methods.First(x => x.Name == targetMethodName);

            //var injectAssembly = AssemblyDefinition.ReadAssembly(injectAssemblyPath);
            //var injectType = injectAssembly.MainModule.GetType(injectTypeName);
            //var injectMethod = injectType.Methods.First(x => x.IsStatic && x.Name == injectMethodName);
            //var injectMethodRef = targetMethod.Module.ImportReference(injectMethod);

            //var processor = targetMethod.Body.GetILProcessor();
            //processor.InsertBefore(targetMethod.Body.Instructions[0], Instruction.Create(OpCodes.Call, injectMethodRef));


            //@this.RunInjectors();

            var modules = @this.Modules;
            var types = modules.SelectMany(m => m.Types);
            var methodes = types.SelectMany(t => t.Methods);
            var parameters = methodes.SelectMany(m => m.Parameters);

            var baseFullName = "SoftCube.Injector.ParameterInjectorAttribute";
            var baseScopeName = "SoftCube.Injector.dll";

            // パラメーターの属性にカスタムコードを注入する。
            foreach (var parameter in parameters)
            {
                foreach (var attribute in parameter.CustomAttributes)
                {
                    var baseAttributeType = attribute.AttributeType.Resolve().BaseType.Resolve();

                    while (baseAttributeType != null && baseAttributeType.BaseType != null)
                    {
                        if (baseAttributeType.FullName == baseFullName && baseAttributeType.Scope.Name == baseScopeName)
                        {
                            var injectorAttribute = attribute.Create<ParameterInjectorAttribute>();
                            injectorAttribute.Inject(parameter);
                            break;
                        }
                        baseAttributeType = baseAttributeType.BaseType.Resolve();
                    }
                }
            }
        }

        #endregion
    }
}

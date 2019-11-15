using Mono.Cecil;
using System.Linq;

namespace SoftCube.Injectors.Runner
{
    /// <summary>
    /// AssemblyDefinitionの拡張メソッド。
    /// </summary>
    internal static class AssemblyDefinitionExtensions
    {
        #region 静的メソッド

        /// <summary>
        /// アセンブリにカスタムコードを注入する。
        /// </summary>
        /// <param name="this">対象アセンブリ</param>
        internal static void Inject(this AssemblyDefinition @this)
        {
            var modules = @this.Modules;
            var types = modules.SelectMany(m => m.Types);
            var methodes = types.SelectMany(t => t.Methods);
            var parameters = methodes.SelectMany(m => m.Parameters);

            var baseFullName = "SoftCube.Injectors.ParameterInjectorAttribute";
            var baseScopeName = "SoftCube.Injectors.dll";

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

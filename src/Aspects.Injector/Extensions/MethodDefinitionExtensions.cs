using Mono.Cecil;
using Mono.Cecil.Pdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftCube.Aspects.Injector
{

    internal static class MethodDefinitionExtensions
    {
        #region 静的メソッド

        /// <summary>
        /// メソッドにカスタムコードを注入する。
        /// </summary>
        /// <param name="method">対象メソッド</param>
        internal static void Inject(this MethodDefinition method)
        {
            // メソッドの属性にカスタムコードを注入する。
            var baseFullName  = $"{nameof(SoftCube)}.{nameof(Aspects)}.{nameof(MethodLevelAspect)}";
            var baseScopeName = $"{nameof(SoftCube)}.{nameof(Aspects)}.dll";

            foreach (var attribute in method.CustomAttributes)
            {
                var baseAttributeType = attribute.AttributeType.Resolve().BaseType.Resolve();

                while (baseAttributeType != null && baseAttributeType.BaseType != null)
                {
                    if (baseAttributeType.FullName == baseFullName && baseAttributeType.Scope.Name == baseScopeName)
                    {
                        var aspect = attribute.Create<MethodLevelAspect>();
                        aspect.Inject(method);
                        break;
                    }

                    baseAttributeType = baseAttributeType.BaseType.Resolve();
                }
            }
        }

        #endregion
    }
}

using Mono.Cecil;
using Mono.Cecil.Pdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftCube.Injectors.Runner
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
            var baseFullName  = "SoftCube.Injectors.MethodInjectorAttribute";
            var baseScopeName = "SoftCube.Injectors.dll";
            foreach (var attribute in method.CustomAttributes)
            {
                var baseAttributeType = attribute.AttributeType.Resolve().BaseType.Resolve();

                while (baseAttributeType != null && baseAttributeType.BaseType != null)
                {
                    if (baseAttributeType.FullName == baseFullName && baseAttributeType.Scope.Name == baseScopeName)
                    {
                        var injectorAttribute = attribute.Create<MethodInjectorAttribute>();
                        injectorAttribute.Inject(method);
                        break;
                    }
                    baseAttributeType = baseAttributeType.BaseType.Resolve();
                }
            }





            //// パラメーターの属性にカスタムコードを注入する。
            //var baseFullName = "SoftCube.Injectors.ParameterInjectorAttribute";
            //var baseScopeName = "SoftCube.Injectors.dll";
            //foreach (var parameter in method.Parameters)
            //{
            //    foreach (var attribute in parameter.CustomAttributes)
            //    {
            //        var baseAttributeType = attribute.AttributeType.Resolve().BaseType.Resolve();

            //        while (baseAttributeType != null && baseAttributeType.BaseType != null)
            //        {
            //            if (baseAttributeType.FullName == baseFullName && baseAttributeType.Scope.Name == baseScopeName)
            //            {
            //                var injectorAttribute = attribute.Create<ParameterInjectorAttribute>();
            //                injectorAttribute.Inject(parameter);
            //                break;
            //            }
            //            baseAttributeType = baseAttributeType.BaseType.Resolve();
            //        }
            //    }
            //}
        }

        #endregion
    }
}

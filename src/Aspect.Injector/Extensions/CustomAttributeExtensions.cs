using Mono.Cecil;
using System;
using System.Linq;

namespace SoftCube.Aspect.Injector
{
    /// <summary>
    /// CustomAttributeの拡張メッソド。
    /// </summary>
    internal static class CustomAttributeExtensions
    {
        #region 静的メソッド

        /// <summary>
        /// CustomAttributeが表現する属性のインスタンスを生成する。
        /// </summary>
        /// <typeparam name="TAttribute">CustomAttributeが表現する属性の型</typeparam>
        /// <param name="this">CustomAttributeのインスタンス</param>
        /// <returns>CustomAttributeが表現する属性のインスタンス</returns>
        internal static TAttribute Create<TAttribute>(this CustomAttribute @this)
            where TAttribute : class
        {
            var type = @this.AttributeType.Resolve();
            var attributeTypeName = type.FullName + ", " + type.Module.Assembly.Name.Name;
            var attributeType = Type.GetType(attributeTypeName);

            // 属性のコンストラクター引数を取得する。
            object[] arguments = null;
            if (@this.HasConstructorArguments)
            {
                arguments = @this.ConstructorArguments.Select(a => a.Value).ToArray();
            }

            // 属性のインスタンスを生成する。
            var value = Activator.CreateInstance(attributeType, arguments) as TAttribute;

            // 属性のプロパティを設定する。
            if (@this.HasProperties)
            {
                foreach (var attributeProperty in @this.Properties)
                {
                    attributeType.GetProperty(attributeProperty.Name).SetValue(value, attributeProperty.Argument.Value, null);
                }
            }

            return value;
        }

        #endregion
    }
}

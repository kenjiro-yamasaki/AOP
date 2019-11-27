using Mono.Cecil;

namespace SoftCube.Aspects.Injector
{
    /// <summary>
    /// TypeDefinitionの拡張メソッド。
    /// </summary>
    internal static class TypeDefinitionExtensions
    {
        #region 静的メソッド

        /// <summary>
        /// アスペクト（カスタムコード）を注入する。
        /// </summary>
        /// <param name="type">注入対象のモジュール</param>
        internal static void Inject(this TypeDefinition type)
        {
            foreach (var method in type.Methods)
            {
                method.Inject();
            }
        }

        #endregion
    }
}

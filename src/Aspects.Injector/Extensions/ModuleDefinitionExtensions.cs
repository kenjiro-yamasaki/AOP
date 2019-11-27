using Mono.Cecil;
using Mono.Cecil.Pdb;
using System.Linq;

namespace SoftCube.Aspects.Injector
{
    /// <summary>
    /// ModuleDefinitionの拡張メソッド。
    /// </summary>
    internal static class ModuleDefinitionExtensions
    {
        #region 静的メソッド

        /// <summary>
        /// モジュールにカスタムコードを注入する。
        /// </summary>
        /// <param name="this">対象モジュール</param>
        internal static void Inject(this ModuleDefinition @this)
        {
            var types = @this.Types;
            var methodes = types.SelectMany(t => t.Methods);

            foreach (var method in types.SelectMany(t => t.Methods))
            {
                method.Inject();
            }
        }

        #endregion
    }
}

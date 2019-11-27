using Mono.Cecil;
using Mono.Cecil.Pdb;
using System.Linq;

namespace SoftCube.Aspects.Injector
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
            foreach (var module in @this.Modules)
            {
                module.Inject();
            }
        }

        #endregion
    }
}

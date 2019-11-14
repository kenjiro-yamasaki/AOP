using Mono.Cecil;

namespace SoftCube.Injector.Runner
{
    /// <summary>
    /// 注入実行者。
    /// </summary>
    public class InjectorRunner
    {
        #region メソッド

        /// <summary>
        /// 注入を実行する。
        /// </summary>
        /// <param name="targetAssemblyPath">注入対象のアセンブリパス</param>
        /// <param name="injectedAssemblyPath">注入後のアセンブリパス</param>
        public void Run(string targetAssemblyPath, string injectedAssemblyPath)
        {
            var assembly = AssemblyDefinition.ReadAssembly(targetAssemblyPath);
            assembly.Inject();
            assembly.Write(injectedAssemblyPath);
        }

        #endregion
    }
}

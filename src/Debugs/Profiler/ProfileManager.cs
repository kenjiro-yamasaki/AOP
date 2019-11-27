using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftCube.Debugs
{
    /// <summary>
    /// プロファイルマネージャー。
    /// </summary>
    public static class ProfileManager
    {
        #region プロパティ

        /// <summary>
        /// プロファイルコレクション。
        /// </summary>
        private static Dictionary<string, Profile> profiles = new Dictionary<string, Profile>();

        #endregion

        #region メソッド

        #region プロファイルコレクション

        /// <summary>
        /// プロファイルを追加する。
        /// </summary>
        /// <param name="profile">プロファイル</param>
        internal static void Add(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            profiles.Add(profile.Name, profile);
        }

        /// <summary>
        /// プロファイルが存在するか？
        /// </summary>
        /// <param name="profileName">プロファイル名</param>
        /// <returns>プロファイルが存在するか？</returns>
        internal static bool Has(string profileName)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(profileName));
            return profiles.ContainsKey(profileName);
        }

        /// <summary>
        /// プロファイルを取得する。
        /// </summary>
        /// <param name="profileName">プロファイル名</param>
        /// <returns>プロファイル</returns>
        internal static Profile Get(string profileName)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(profileName));
            Assert.IsTrue(profiles.ContainsKey(profileName));
            Assert.IsTrue(profiles[profileName] != null);

            return profiles[profileName];
        }

        #endregion

        /// <summary>
        /// プロファイル結果をログ出力する。
        /// </summary>
        public static void OutputLog()
        {
            Logger.Trace("----- プロファイル結果 -----");
            foreach (Profile profile in profiles.Values.OrderByDescending(p => p.TotalTime))
            {
                Assert.IsTrue(profile != null);
                Assert.IsTrue(1 <= profile.Count);
                profile.OutputLog();
            }
        }

        #endregion
    }
}

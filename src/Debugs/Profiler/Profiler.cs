using System;

namespace SoftCube.Debugs
{
    /// <summary>
    /// プロファイラー。
    /// </summary>
    public class Profiler
        : IDisposable
    {
        #region プロパティ

        /// <summary>
        /// プロファイル名。
        /// </summary>
        private string ProfileName { get; set; }

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="profileName">プロファイル名</param>
        public Profiler(string profileName)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(profileName));

            ProfileName = profileName;
            Assert.IsTrue(!string.IsNullOrEmpty(ProfileName));

            if (ProfileManager.Has(ProfileName) == false)
            {
                ProfileManager.Add(new Profile(ProfileName));
            }
            ProfileManager.Get(ProfileName).Start();
        }

        #endregion

        #region メソッド

        #region IDisposable

        /// <summary>
        /// ファイナライザー。
        /// </summary>
        ~Profiler()
        {
            Dispose(false);
        }

        /// <summary>
        /// 破棄する。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 破棄する。
        /// </summary>
        /// <param name="disposing">IDisposable.Dispose()から呼び出されたか</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(ProfileName));
                ProfileManager.Get(ProfileName).Stop();
            }
        }

        #endregion

        /// <summary>
        /// キャンセルする。
        /// </summary>
        public void Cancel()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(ProfileName));
            ProfileManager.Get(ProfileName).Cancel();
        }

        #endregion
    }
}

using System.Diagnostics;

namespace SoftCube.Debugs
{
    /// <summary>
    /// プロファイル記録。
    /// </summary>
    internal class Profile
    {
        #region プロパティ

        /// <summary>
        /// プロファイル名。
        /// </summary>
        internal string Name { get; }

        /// <summary>
        /// プロファイル回数。
        /// </summary>
        internal int Count { get; set; }

        /// <summary>
        /// 合計プロファイル時間（単位：タイマー刻み）。
        /// </summary>
        internal long TotalTime { get; set; }

        /// <summary>
        /// 最小プロファイル時間（単位：タイマー刻み）。
        /// </summary>
        private long MinTime { get; set; }

        /// <summary>
        /// 最大プロファイル時間（単位：タイマー刻み）。
        /// </summary>
        private long MaxTime { get; set; }

        /// <summary>
        /// 最小プロファイル順序（1～）。
        /// </summary>
        private int MinSequence { get; set; }

        /// <summary>
        /// 最大プロファイル順序（1～）。
        /// </summary>
        private int MaxSequence { get; set; }

        /// <summary>
        /// キャンセルフラグ。
        /// </summary>
        private bool Canceled { get; set; }

        /// <summary>
        /// ストップウォッチ。
        /// </summary>
        private Stopwatch Stopwatch { get; } = new Stopwatch();

        #endregion

        #region コンストラクター

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="name">プロファイル名</param>
        public Profile(string name)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(name));

            Name = name;
        }

        #endregion

        #region メソッド

        /// <summary>
        /// プロファイルを開始する。
        /// </summary>
        public void Start()
        {
            Stopwatch.Restart();
            Canceled = false;
        }

        /// <summary>
        /// プロファイルを終了する。
        /// </summary>
        public void Stop()
        {
            if (Canceled)
            {
                Canceled = false;
            }
            else
            {
                Stopwatch.Stop();
                long elapsedTime = Stopwatch.ElapsedTicks;
                TotalTime += elapsedTime;

                if (Count == 0)
                {
                    MinTime = elapsedTime;
                    MinSequence = Count + 1;

                    MaxTime = elapsedTime;
                    MaxSequence = Count + 1;
                }
                else
                {
                    if (elapsedTime < MinTime)
                    {
                        MinTime = elapsedTime;
                        MinSequence = Count + 1;
                    }
                    if (MaxTime < elapsedTime)
                    {
                        MaxTime = elapsedTime;
                        MaxSequence = Count + 1;
                    }
                }
                Count++;
            }
        }

        /// <summary>
        /// プロファイルをキャンセルする。
        /// </summary>
        public void Cancel()
        {
            Canceled = true;
        }

        /// <summary>
        /// プロファイル結果をログ出力する。
        /// </summary>
        public void OutputLog()
        {
            Assert.IsTrue(1 <= Count);

            var totalTime   = $"{TotalTime / (double)Stopwatch.Frequency:0.0000000000}";
            var averageTime = $"{TotalTime / Count / (double)Stopwatch.Frequency:0.0000000000}";
            var maxTime     = $"{MaxTime / (double)Stopwatch.Frequency:0.0000000000}";
            var minTime     = $"{MinTime / (double)Stopwatch.Frequency:0.0000000000}";
            var maxSequence = $"{MaxSequence}";
            var minSequence = $"{MinSequence}";
            var count       = $"{Count}";

            Logger.Trace($"{Name} **************************");
            Logger.Trace($"合計プロファイル時間={totalTime}s");
            Logger.Trace($"平均プロファイル時間={averageTime}s");
            Logger.Trace($"最大プロファイル時間={maxTime}s");
            Logger.Trace($"最小プロファイル時間={minTime}s");
            Logger.Trace($"最大プロファイル順序={maxSequence}");
            Logger.Trace($"最小プロファイル順序={minSequence}");
            Logger.Trace($"プロファイル回数={count}");
        }

        #endregion
    }
}

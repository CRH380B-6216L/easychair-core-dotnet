namespace EasyInterlock
{
    /// <summary>
    /// 定义会议时轴。实现该类型需要外部 Timer 控件支持。
    /// </summary>
    public class Clock
    {
        /// <summary>
        /// 会议时轴的当前时刻。
        /// </summary>
        public DateTime Current { get; set; }
        /// <summary>
        /// 会议时轴与现实时间的倍率值。
        /// </summary>
        public int ClockRate { get; set; }
        /// <summary>
        /// 如果休会期间继续时轴，用于记录休会的时刻、并在下个会期开始时计算休会期间会议流逝的时间。
        /// </summary>
        public DateTime? SavedAt { get; set; }

        /// <summary>
        /// 以默认值初始化会议时轴。
        /// </summary>
        public Clock() 
        {
            Current = DateTime.Now;
            ClockRate = 1;
        }

        /// <summary>
        /// 以特定日期和时间初始化会议时轴。
        /// </summary>
        /// <param name="startDate">时轴的开始时间。</param>
        public Clock(DateTime startDate) : this() 
        {
            Current = startDate;
        }

        /// <summary>
        /// 以特定日期时间和时轴倍率初始化会议时轴。
        /// </summary>
        /// <param name="startDate">时轴的开始时间。</param>
        /// <param name="clockRate">会议时轴与现实时间的倍率值。</param>
        public Clock(DateTime startDate, int clockRate) : this(startDate)
        {
            ClockRate = clockRate;
        }

        /// <summary>
        /// 启动会议时轴。如果设置时轴停止期间继续，将同时加算期间流逝的时间。
        /// </summary>
        public void Up()
        {
            if (SavedAt != null)
            {
                TimeSpan elapsed = (TimeSpan)(DateTime.Now - SavedAt) * ClockRate;
                Current.Add(elapsed);
            }
            SavedAt = null;
        }

        /// <summary>
        /// 停止会议时轴。
        /// </summary>
        /// <param name="isContinueUponDown">定义休会期间是否继续时轴。如果值为 true，将在下次启动会议时轴时将流逝的时间加算。</param>
        public void Down(bool isContinueUponDown)
        {
            if (isContinueUponDown)
            {
                SavedAt = DateTime.Now;
            }
        }

        /// <summary>
        /// 更新会议时轴。需要外部 Timer 控件支持，并在 Timer.Tick 事件发生时使用该方法。
        /// </summary>
        /// <param name="interval">指定在相对于上一次发生的 Tick 事件引发 Tick 事件之前的毫秒数。该值不能小于 1。</param>
        public void Tick(int interval)
        {
            Current = Current.AddMilliseconds(interval * ClockRate);
        }
    }
}
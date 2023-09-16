namespace EasyInterlock
{
    public class Clock
    {
        public DateTime Current { get; set; }
        public int ClockRate { get; set; }
        public DateTime? SavedAt { get; set; }

        public Clock() 
        {
            Current = DateTime.Now;
            ClockRate = 1;
        }

        public Clock(DateTime startDate) : this() 
        {
            Current = startDate;
        }

        public Clock(DateTime startDate, int clockRate) : this(startDate)
        {
            ClockRate = clockRate;
        }

        public void Up()
        {
            if (SavedAt != null)
            {
                TimeSpan elapsed = (TimeSpan)(DateTime.Now - SavedAt) * ClockRate;
                Current.Add(elapsed);
            }
            SavedAt = null;
        }

        public void Down(bool isContinueUponDown)
        {
            if (isContinueUponDown)
            {
                SavedAt = DateTime.Now;
            }
        }

        public void Tick(int interval)
        {
            Current = Current.AddMilliseconds(interval * ClockRate);
        }
    }
}
namespace Report.Web.EF
{
    public class NiftyWeekly
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double DayLowToHigh { get; set; }
        public double PrevDayClose { get; set; }
        public double HighFrmY { get; set; } // high - open
        public double LowFrmY { get; set; }  // open- low
        public double CloseFrmY { get; set; }  // open -close
        public double _10AM { get; set; }
        public double _10_30AM { get; set; }

        public double _1PM { get; set; }

        public double _2PM { get; set; }

        public double _2_25PM { get; set; }

        public double Gap { get; set; }
        public DateTime DayMaxHighReachedAt { get; set; }
        public DateTime DayMaxLowReachedAt { get; set; }

    }
}

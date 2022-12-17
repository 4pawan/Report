using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.Web.EF
{
    public class NiftyWeekly
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Open { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double High { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Low { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Close { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "LowHigh")]
        public double DayLowToHigh { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "PreClose")]
        public double PrevDayClose { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double HighFrmY { get; set; } // high - open

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double LowFrmY { get; set; }  // open- low

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double CloseFrmY { get; set; }  // open -close

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double _10AM { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double _10_30AM { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double _1PM { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double _2PM { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double _2_25PM { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Gap { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        [Display(Name = "HighTime")]
        public DateTime DayMaxHighReachedAt { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        [Display(Name = "LowTime")]
        public DateTime DayMaxLowReachedAt { get; set; }

        [NotMapped]
        public string DayMaxHighReachedClass
        {
            get
            {
                string green = "green";
                var isBefore10Am = TimeSpan.Compare(DayMaxHighReachedAt.TimeOfDay, new TimeSpan(10, 1, 2));

                if (isBefore10Am == -1)
                {
                    return green;
                }
                if (isBefore10Am == 0)
                {
                    return green;
                }

                var isAfter03Pm = TimeSpan.Compare(DayMaxHighReachedAt.TimeOfDay, new TimeSpan(15, 0, 0));
                if (isAfter03Pm != -1)
                {
                    return "lightgreen";
                }


                return "yellow";
            }
        }



        [NotMapped]
        public string DayMaxLowReachedClass
        {
            get
            {
                string green = "green";
                var isBefore10Am = TimeSpan.Compare(DayMaxLowReachedAt.TimeOfDay, new TimeSpan(10, 1, 2));

                if (isBefore10Am == -1)
                {
                    return green;
                }
                if (isBefore10Am == 0)
                {
                    return green;
                }

                var isAfter03Pm = TimeSpan.Compare(DayMaxLowReachedAt.TimeOfDay, new TimeSpan(15, 0, 0));
                if (isAfter03Pm != -1)
                {
                    return "lightgreen";
                }


                return "yellow";
            }
        }






        [NotMapped]
        public string IsRuleMatchedClass
        {
            get
            {
                //rule 1: if gap down thn closing should be -ve or not more thn gap +50 
                if (Gap < 0)
                {
                    if (CloseFrmY < 0 || Gap + 50 >= CloseFrmY)
                    {
                        return "orange";
                    }

                    return "lightcoral";
                }
                return "";
            }
        }
    }
}

using Newtonsoft.Json;
using Report.Web.ViewModels;
using Report.Web.EF;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;

namespace Report.Web.helper
{
    public static class SyncDB
    {
        public static CandleData ReadMonthlyData(string path, ApplicationDbContext dbContext)
        {
            var data = ReadJson(path);
            var candleList = new List<NiftyWeekly>();

            // get all formating done with all calculations
            int count = 0;
            foreach (List<object> c in data.data.candles)
            {
                var _date = DateTime.Parse(Convert.ToString(c[0]));
                count++;

                if (WeeklyNonThursdayDates.TradingDates.All(d => d.Date != _date))
                {
                    if (!IsThursday(_date) || dbContext.NiftyWeekly.Any(d => d.Date == _date))
                    {
                        continue;
                    }
                }
                
                var candle = new NiftyWeekly();
                candle.Date = _date;
                double Open = Convert.ToDouble(c[1]);
                double High = Convert.ToDouble(c[2]);
                double Low = Convert.ToDouble(c[3]);
                double Close = Convert.ToDouble(c[4]);
                double DayLowToHigh = High - Low;
                double PrevDayClose = candleList.Any() ? Convert.ToDouble(data.data.candles[count - 2][4]) : 0;
                candle.Open = Open;
                candle.High = High;
                candle.Low = Low;
                candle.Close = Close;
                //candle.Volume = long.Parse(c[5].ToString());
                candle.DayLowToHigh = DayLowToHigh;
                candle.PrevDayClose = PrevDayClose;
                candle.Gap = Open - PrevDayClose;
                candle.HighFrmY = High - PrevDayClose;
                candle.LowFrmY = Low - PrevDayClose;
                candle.CloseFrmY = Close - PrevDayClose;
                //candle.CentHighFrmY = ((High - PrevDayClose) / PrevDayClose) * 100;
                //candle.CentLowFrmY = ((Low - PrevDayClose) / PrevDayClose) * 100;
                //candle.CentCloseFrmY = ((Close - PrevDayClose) / PrevDayClose) * 100;
                //candle.DayCentLowToHigh = (DayLowToHigh / Low) * 100;
                candleList.Add(candle);
            }

            if (candleList.Any())
            {
                dbContext.NiftyWeekly.AddRange(candleList);
                dbContext.SaveChanges(true);
            }

            return null;
        }

        public static CandleData Sync5minData(string path, ApplicationDbContext dbContext)
        {
            var data = ReadJson(path);
            var candleList = new List<NiftyWeekly>();
            var existingDbList = dbContext.NiftyWeekly.Where(d => d._10AM == 0);

            foreach (var c in existingDbList)
            {
                var dayEntries = new List<NiftyWeekly>();
                foreach (var item in data.data.candles)
                {
                    if (Convert.ToDateTime(item[0]).Date == c.Date)
                    {
                        var _date = DateTime.Parse(Convert.ToString(item[0]));
                        double High = Convert.ToDouble(item[2]);
                        double Low = Convert.ToDouble(item[3]);
                        double Close = Convert.ToDouble(item[4]);

                        dayEntries.Add(new NiftyWeekly
                        {
                            Date = _date,
                            High = High,
                            Low = Low,
                            Close = Close
                        });
                    }
                }


                if (!dayEntries.Any())
                    continue;

                var max = dayEntries.Max(r => r.High);
                var min = dayEntries.Min(r => r.Low);

                var _10AM = dayEntries.FirstOrDefault(r => r.Date.ToShortTimeString() == "10:00 AM");
                var _10_30AM = dayEntries.FirstOrDefault(r => r.Date.ToShortTimeString() == "10:30 AM");
                var _1PM = dayEntries.FirstOrDefault(r => r.Date.ToShortTimeString() == "01:00 PM");
                var _2PM = dayEntries.FirstOrDefault(r => r.Date.ToShortTimeString() == "02:00 PM");
                var _2_25PM = dayEntries.FirstOrDefault(r => r.Date.ToShortTimeString() == "02:25 PM");
                
                if (_10AM != null)
                    c._10AM = _10AM.Close - c.PrevDayClose;
                if (_10_30AM != null)
                    c._10_30AM = _10_30AM.Close - c.PrevDayClose;
                if (_1PM != null)
                    c._1PM = _1PM.Close - c.PrevDayClose;
                if (_2PM != null)
                    c._2PM = _2PM.Close - c.PrevDayClose;
                if (_2_25PM != null)
                    c._2_25PM = _2_25PM.Close - c.PrevDayClose;

                c.DayMaxHighReachedAt = dayEntries.First(d=>d.High == max).Date ;   
                c.DayMaxLowReachedAt = dayEntries.First(d => d.Low == min).Date; 
                candleList.Add(c);
            }

            if (candleList.Any())
            {
                dbContext.NiftyWeekly.UpdateRange(candleList);
                dbContext.SaveChanges(true);
            }

            return null;
        }

        public static CandleData ReadJson(string path)
        {
            var json = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<CandleData>(json);
            return data;
        }

        public static bool IsThursday(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Thursday;
        }

        private static bool IsDateFound(object o)
        {
            return false;
        }
    }

}

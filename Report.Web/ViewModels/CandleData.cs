namespace Report.Web.ViewModels
{
    public class CandleData
    {
        public string status { get; set; }
        public Candle data { get; set; }

    }

    public class Candle
    {
        public List<List<object>> candles { get; set; }
    }


}

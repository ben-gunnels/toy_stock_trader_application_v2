public class Stock
{
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public float Price { get; set; }
    public float Open { get; set; }
    public float High { get; set; }
    public float Low { get; set; }
    public double Volume { get; set; }
    public float PreviousClose { get; set; }
    public float Change { get; set; }
    public float ChangePercent { get; set; }
    public float PreOrPostMarket { get; set; }
    public float PreOrPostMarketChange { get; set; }
    public float PreOrPostMarketChangePercent { get; set; }
    public string LastUpdateUtc { get; set; }

    public Stock(string symbol, string name, string type, float price,
                 float open, float high, float low, double volume, float previousClose,
                 float change, float changePercent, float preOrPostMarket, float preOrPostMarketChange,
                 float preOrPostMarketChangePercent, string lastUpdateUtc)
    {
        this.Symbol = symbol;
        this.Name = name;
        this.Type = type;
        this.Price = price;
        this.Open = open;
        this.High = high;
        this.Low = low;
        this.Volume = volume;
        this.PreviousClose = previousClose;
        this.Change = change;
        this.ChangePercent = changePercent;
        this.PreOrPostMarket = preOrPostMarket;
        this.PreOrPostMarketChange = preOrPostMarketChange;
        this.PreOrPostMarketChangePercent = preOrPostMarketChangePercent;
        this.LastUpdateUtc = lastUpdateUtc;
    }
}
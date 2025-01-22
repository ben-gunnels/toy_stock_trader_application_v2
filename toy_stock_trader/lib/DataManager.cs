

using System.Text.Json;

namespace DataManager {
    class DataManager {
        // Manages data in the backend for the client
        // Saves the client from making unnecessary calls to the API
        // Manages encryption and decryption of passwords on behalf of the user
        private API api;   

        async public Task<Stock> TakeStockLookup(string symbol, Cache.Cache cache) {
            Stock stock = cache.GetStock(symbol);
            if (stock != null)
            {
                return stock;
            }

            string res = await MakeAPIQuery(symbol);

            stock = this.ResToStock(res);
            return stock;
        }


        async private Task<string> MakeAPIQuery(string symbol) {
            string body = "";
            body = await api.MakeRequest(symbol);
            return body;
        }

        private Stock ResToStock(string res) {
            var stock = JsonSerializer.Deserialize<Dictionary<string, string>>(res);

            string symbol = stock["symbol"];
            string name = stock["name"];
            string type = stock["type"];
            float price = float.Parse(stock["price"]);
            float open = float.Parse(stock["open"]);
            float high = float.Parse(stock["high"]);
            float low = float.Parse(stock["low"]);
            float volume = float.Parse(stock["volume"]);
            float previousClose = float.Parse(stock["previousClose"]);
            float change = float.Parse(stock["change"]);
            float changePercent = float.Parse(stock["changePercent"]);
            float preOrPostMarket = float.Parse(stock["preOrPostMarket"]);
            float preOrPostMarketChange = float.Parse(stock["preOrPostMarketChange"]);
            float preOrPostMarketChangePercent = float.Parse(stock["preOrPostMarketChangePercent"]);
            string lastUpdateUtc = stock["lastUpdateUtc"];




            return new Stock(symbol, name, type, price, open,
                             high, low, volume, previousClose, change, changePercent,
                             preOrPostMarket, preOrPostMarketChange, preOrPostMarketChangePercent, lastUpdateUtc);
        }
    }
}
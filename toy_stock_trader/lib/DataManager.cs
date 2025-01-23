using System.Text.Json;

namespace DataManager {
    class DataManager {
        // Manages data in the backend for the client
        // Saves the client from making unnecessary calls to the API
        // Manages encryption and decryption of passwords on behalf of the user
        private API api;   
        private Cache.Cache cache;
        private string SecretKey = Environment.GetEnvironmentVariable("RapidApiKey");

        public DataManager(API api, Cache.Cache cache) {
            this.api = api;
            this.cache = cache;
        }

        async public Task<Stock> TakeStockLookup(string symbol) {
            Console.WriteLine("Searching Database...");
            Stock stock = cache.GetStock(symbol);
            if (stock != null)
            {
                return stock;
            }

            Console.WriteLine("Calling API...");
            
            string res = await MakeAPIQuery(symbol);
            stock = this.ResToStock(res);
            // Console.WriteLine(res);

            this.cache.AddStock(symbol, stock);
            return stock;
        }


        async private Task<string> MakeAPIQuery(string symbol) {
            string body = "";
            body = await api.MakeRequest(symbol);
            return body;
        }

        private Stock ResToStock(string res) {
            var json = JsonSerializer.Deserialize<Dictionary<string, object>>(res);

            // Dereference the "data" key
            if (json != null && json.TryGetValue("data", out var dataObj))
            {
                // Deserialize the "data" section into a dictionary with dynamic values
                var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(dataObj.ToString());

                // Extract individual stock attributes with type-specific handling
                string symbol = data["symbol"].GetString();
                string name = data["name"].GetString();
                string type = data["type"].GetString();
                float price = data["price"].GetSingle();
                float open = data["open"].GetSingle();
                float high = data["high"].GetSingle();
                float low = data["low"].GetSingle();
                float volume = data["volume"].GetSingle();
                float previousClose = data["previous_close"].GetSingle();
                float change = data["change"].GetSingle();
                float changePercent = data["change_percent"].GetSingle();
                float preOrPostMarket = data["pre_or_post_market"].GetSingle();
                float preOrPostMarketChange = data["pre_or_post_market_change"].GetSingle();
                float preOrPostMarketChangePercent = data["pre_or_post_market_change_percent"].GetSingle();
                string lastUpdateUtc = data["last_update_utc"].GetString();

                return new Stock(symbol, name, type, price, open, high, low, volume, previousClose, change, changePercent,
                                preOrPostMarket, preOrPostMarketChange, preOrPostMarketChangePercent, lastUpdateUtc);
            }
            else
            {
                throw new Exception("The 'data' key was not found in the JSON response.");
            }
        }
    }
}
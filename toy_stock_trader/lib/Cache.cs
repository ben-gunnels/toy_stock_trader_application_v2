


namespace Cache {
    class Cache { 
        // Caches stock data to avoid extraneous requests

        public Dictionary<string, Stock> cache = new Dictionary<string, Stock>();

        public bool StockAvailable(string symbol) {
            if (cache.TryGetValue(symbol, out Stock stock)) {
                return true;
            }
            return false;
        }


        public Stock GetStock(string symbol) {
            if (this.StockAvailable(symbol)) 
            {
                Console.WriteLine($"Stock {symbol} exists in database.\n");
                return cache[symbol];
            }
            Console.WriteLine("Stock does not exist in database. Make an API request.\n");
            return null;
        }
    }
}
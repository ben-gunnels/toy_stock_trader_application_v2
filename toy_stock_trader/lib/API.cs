using System.Net.Http.Headers;

class API {
    private HttpClient client = new HttpClient();

    private HttpRequestMessage FormatRequest(string symbol) {
        return new HttpRequestMessage {
        Method = HttpMethod.Get,
        RequestUri = new Uri($"https://real-time-finance-data.p.rapidapi.com/stock-quote?symbol={symbol}&language=en"),
        Headers =
        {
            { "x-rapidapi-key", "7192e629f7mshab104f66a8cb883p1828ffjsncb88409168d4" },
            { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
        },
    };
    }

    async public Task<string> MakeRequest(string symbol) {
        /*  
            Params: symbol = desired stock symbol to purchase or sell
            Returns: body = json object of stock data queried
        
        */
        HttpRequestMessage request = FormatRequest(symbol);
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }

}
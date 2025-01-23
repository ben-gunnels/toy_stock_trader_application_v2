using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class API
{
    private string SecretKey = Environment.GetEnvironmentVariable("RapidApiKey");

    public async Task<string> MakeRequest(string symbol) {
        Console.WriteLine($"Rapid Key: {SecretKey}");
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://real-time-finance-data.p.rapidapi.com/stock-quote?symbol={symbol}&language=en"),
            Headers =
            {
                { "x-rapidapi-key", SecretKey },
                { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }

}
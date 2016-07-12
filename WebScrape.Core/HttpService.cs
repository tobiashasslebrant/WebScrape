using System.Net.Http;

namespace WebScrape.Core
{
    public class HttpService
    {
        readonly HttpClient _client = new HttpClient();
        public string GetStringAsync(string requestUri) 
            => _client.GetStringAsync(requestUri).Result;
    }
}
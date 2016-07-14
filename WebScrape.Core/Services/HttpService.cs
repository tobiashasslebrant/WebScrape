using System.Net.Http;

namespace WebScrape.Core.Services
{
    public interface IHttpService
    {
        string GetStringAsync(string requestUri);
    }

    public class HttpService : IHttpService
    {
        readonly HttpClient _client = new HttpClient();
        public string GetStringAsync(string requestUri) 
            => _client.GetStringAsync(requestUri).Result;

    }
}
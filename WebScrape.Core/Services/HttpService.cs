using System.Net.Http;
using System.Threading.Tasks;

namespace WebScrape.Core.Services
{
    public interface IHttpService
    {
        Task<string> GetStringAsync(string requestUri);
        string GetString(string requestUri, int delay);
    }

    public class HttpService : IHttpService
    {
        readonly HttpClient _client = new HttpClient();

        public async Task<string> GetStringAsync(string requestUri) 
            => await _client.GetStringAsync(requestUri);

        public string GetString(string requestUri, int delay)
        {
            if (delay > 0)
                System.Threading.Thread.Sleep(delay);
            return GetStringAsync(requestUri).Result;
        }

    }
}
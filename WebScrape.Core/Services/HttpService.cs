using System.Net.Http;
using System.Threading.Tasks;

namespace WebScrape.Core.Services
{
    public interface IHttpService
    {
        Task<string> GetStringAsync(string requestUri);
    }

    public class HttpService : IHttpService
    {
        readonly HttpClient _client = new HttpClient();
        public async Task<string> GetStringAsync(string requestUri) 
            => await _client.GetStringAsync(requestUri);

    }
}
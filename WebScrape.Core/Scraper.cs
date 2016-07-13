using WebScrape.Core.Models;
using WebScrape.Core.Services;

namespace WebScrape.Core
{
    public class Scraper
    {
        private readonly ScrapeConfiguration _scrapeConfiguration;
        private readonly IFileService _fileService;
        private readonly IHttpService _httpService;

        public Scraper(ScrapeConfiguration scrapeConfiguration, IFileService fileService, IHttpService httpService)
        {
            _scrapeConfiguration = scrapeConfiguration;
            _fileService = fileService;
            _httpService = httpService;
        }

        public ResultScraped Scrape ()
        {
            var html = GetHtml(CacheType.Result, _scrapeConfiguration.Path, 0);

            var scraped = new ResultScraped();
            var htmlItems = _scrapeConfiguration.ItemsIdentifier.Elements(html);
            var index = 0;
            
            foreach (var htmlItem in htmlItems)
            {
                var item = htmlItem;
                if (_scrapeConfiguration.FollowItemLink)
                {
                    var itemLink =_scrapeConfiguration.ItemLinkIdentifier.Attr("href", html);

                    if (itemLink == null) continue;
                    item = GetHtml(CacheType.Item, itemLink, index);
                    index++;
                }

                scraped.NewItem();
                foreach (var identifier in _scrapeConfiguration.ResultItemsIdentifiers)
                    scraped.AddValue(identifier.Element(item));
            }
            return scraped;
        }

        string GetHtml(CacheType cacheType, string path, int index)
        {
            string html;
            if (_scrapeConfiguration.UseCache)
            {
                var filePath = _fileService.UniqueFileName(cacheType, path, index);
                if (_fileService.Exists(filePath))
                    html = _fileService.Read(filePath);
                else
                {
                    html = _httpService.GetStringAsync(_scrapeConfiguration.Path);
                    _fileService.Write(filePath, html);
                }
            }
            else
            {
                html = _httpService.GetStringAsync(_scrapeConfiguration.Path);
            }
            return html;
        }
    }
}
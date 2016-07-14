using System;
using System.Threading.Tasks;
using WebScrape.Core.Models;
using WebScrape.Core.Services;

namespace WebScrape.Core
{
    public class Scraper
    {
        readonly ScrapeConfiguration _scrapeConfiguration;
        readonly IFileService _fileService;
        readonly IHttpService _httpService;

        public Scraper(ScrapeConfiguration scrapeConfiguration, IFileService fileService, IHttpService httpService)
        {
            _scrapeConfiguration = scrapeConfiguration;
            _fileService = fileService;
            _httpService = httpService;
        }

        public async Task<ResultScraped> ScrapeAsync ()
        {
            var html = await GetHtmlAsync(CacheType.Result, _scrapeConfiguration.Path, 0);

            var scraped = new ResultScraped();
            var htmlItems = _scrapeConfiguration.ItemsParser.Elements(html);
            var index = 0;
            
            foreach (var htmlItem in htmlItems)
            {
                var item = htmlItem;
                scraped.NewItem();
                try
                {
                    if (_scrapeConfiguration.FollowItemLink)
                    {
                        var itemLink = _scrapeConfiguration.ItemLinkParser.Attr("href", htmlItem);

                        if (itemLink == null) continue;
                        item = await GetHtmlAsync(CacheType.Item, itemLink, index);
                        index++;
                    }

                    foreach (var identifier in _scrapeConfiguration.FieldParsers)
                        scraped.AddValue(identifier.Element(item));
                }
                catch (Exception)
                {
                    scraped.AddValue("ERROR: Could not process item");
                }
            }
            return scraped;
        }

        async Task<string> GetHtmlAsync(CacheType cacheType, string path, int index)
        {
            string html;
            if (_scrapeConfiguration.UseCache)
            {
                var filePath = _fileService.UniqueFileName(cacheType, path, index);
                if (_fileService.Exists(filePath))
                    html = _fileService.Read(filePath);
                else
                {
                    html = await _httpService.GetStringAsync(path);
                    _fileService.Write(filePath, html);
                }
            }
            else
            {
                html = await _httpService.GetStringAsync(path);
            }
            return html;
        }
    }
}
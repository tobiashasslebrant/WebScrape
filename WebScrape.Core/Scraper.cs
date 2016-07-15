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

        public async Task<ResultScraped> ScrapeAsync (string path)
        {
            var html = await GetHtmlAsync(CacheType.Result, path);

            var scraped = new ResultScraped();
            var htmlItems = _scrapeConfiguration.ItemsParser.Elements(html);
            
            foreach (var htmlItem in htmlItems)
            {
                scraped.NewItem();
                try
                {
                    string item;
                    if (_scrapeConfiguration.FollowItemLink)
                    {
                        var itemLink = _scrapeConfiguration.ItemLinkParser.Attr("href", htmlItem);
                        if (itemLink == null)
                            continue;

                        item = await GetItemHtmlAsync(itemLink);
                    }
                    else
                        item = htmlItem;

                    foreach (var fieldParser in _scrapeConfiguration.FieldParsers)
                        scraped.AddValue(fieldParser.Element(item));
                }
                catch (Exception)
                {
                    scraped.AddValue("ERROR: Could not process item");
                }
            }
            return scraped;
        }

        async Task<string> GetItemHtmlAsync(string path)
        {
            if (_scrapeConfiguration.RequestDelay > 0)
                System.Threading.Thread.Sleep(_scrapeConfiguration.RequestDelay);

            string html;

            if (_scrapeConfiguration.UseAsync)
                html = await GetHtmlAsync(CacheType.Item, path);
            else
                html = GetHtmlAsync(CacheType.Item, path).Result;

            return html;
        }

        async Task<string> GetHtmlAsync(CacheType cacheType, string path)
        {
            string html;
            if (_scrapeConfiguration.UseCache)
            {
                var filePath = _fileService.UniqueFileName(cacheType, path);
                var fileExists = await _fileService.ExistsAsync(filePath);
                if (fileExists)
                    html = await _fileService.ReadAsync(filePath);
                else
                {
                    html = await _httpService.GetStringAsync(path);
                    await _fileService.WriteAsync(filePath, html);
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
using System;
using System.IO;
using WebScrape.Core;

namespace WebScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            var settingsFile = File.ReadAllText("WebScraper.json");
            var format = new ScrapeFormat(settingsFile);
            var scraper = new Scraper(format, new FileService(), new HttpService(), new CssParser());
            var scrapedData = scraper.Scrape();
            var writer = new Writer(Console.Out);
            writer.Write(scrapedData, format.FieldDelimiter);
        }
    }
}

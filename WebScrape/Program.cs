using System;
using System.IO;
using WebScrape.Core;
using WebScrape.Core.HtmlParsers;

namespace WebScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            var settingsFile = File.ReadAllText("WebScraper.json");
            var format = new ScrapeFormat(settingsFile);
            if (args.Length == 1)
                format.Path = args[0];
            var scraper = new Scraper(format, new FileService(), new HttpService());
            var scrapedData = scraper.Scrape();
            var writer = new Writer(Console.Out);
            writer.Write(scrapedData, format.FieldDelimiter);
        }
    }
}

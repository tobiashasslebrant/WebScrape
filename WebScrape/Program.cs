using System;
using System.IO;
using WebScrape.Core;
using WebScrape.Core.Services;

namespace WebScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            var format = new ScrapeConfiguration();
            format.Load(File.ReadAllText("WebScraper.json"));

            if (args.Length == 1)
                format.Path = args[0];
            var scraper = new Scraper(format, new FileService(), new HttpService());
            var scrapedData = scraper.Scrape();
            var writer = new Writer(Console.Out);
            writer.Write(scrapedData, format.FieldDelimiter);
        }
    }
}

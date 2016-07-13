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
            var arguments = new Arguments(args);
            if (arguments.ShowHelp)
            {
                Console.Out.Write(arguments.ShowHelp);
                return;
            }

            var format = new ScrapeConfiguration();
            if (arguments.Path != "")
                format.Path = arguments.Path;

            format.LoadJson(File.ReadAllText(arguments.Settings));

            var scraper = new Scraper(format, new FileService(), new HttpService());
            var scrapedData = scraper.Scrape();
            var writer = new Writer(Console.Out);
            writer.Write(scrapedData, format.FieldDelimiter);
        }
    }
}

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
                Console.Out.Write(arguments.HelpText);
                return;
            }
            
            var configuration = new ScrapeConfiguration();
            var configurationJson = File.ReadAllText(arguments.ConfigurationPath ?? "WebScrape.json");
            configuration.load(configurationJson);

            if (arguments.Path != null)
                configuration.Path = arguments.Path;
            
            var scraper = new Scraper(configuration, new FileService(), new HttpService());
            var scrapedData = scraper.Scrape();
            var writer = new Writer(Console.Out);
            writer.Write(scrapedData, configuration.FieldDelimiter);
        }
    }
}

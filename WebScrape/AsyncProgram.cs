using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebScrape.Core;
using WebScrape.Core.Models;
using WebScrape.Core.Services;

namespace WebScrape
{
    public class AsyncProgram
    {
        public async Task Main(string[] args)
        {
            var arguments = new Arguments(args);
            if (arguments.ShowHelp)
            {
                Console.Out.Write(arguments.HelpText);
                return;
            }
            if (arguments.ShowExample)
            {
                Console.Out.Write(arguments.ExampleText);
                return;
            }
            if (arguments.ShowHelpConfiguration)
            {
                Console.Out.Write(arguments.HelpConfigurationText);
                return;
            }

            try
            {
                var configuration = new ScrapeConfiguration();
                var configurationJson = File.ReadAllText(arguments.ConfigurationPath ?? "WebScrape.json");
                configuration.load(configurationJson);

                var scraper = new Scraper(configuration, new FileService(), new HttpService());
                var scraped = new List<ResultScraped>();

                var rawPath = arguments.Path ?? configuration.Path;
                foreach (var path in Utility.EnumeratePath(rawPath))
                {
                    if (configuration.UseAsync)
                    {
                        var scrapedData = await scraper.ScrapeAsync(path);
                        scraped.Add(scrapedData);
                    }
                    else
                    {
                        scraped.Add(scraper.ScrapeAsync(path).Result);
                    }
                }

                var writer = new Writer(Console.Out);
                foreach (var scrapedData in scraped)
                    writer.Write(scrapedData, configuration.FieldDelimiter);
            }
            catch (Exception exception)
            {
                Console.Out.Write(exception.Message);
            }
        }
    }
}
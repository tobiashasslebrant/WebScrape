using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebScrape.Core;
using WebScrape.Core.Models;
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
            
            Task.Run(async () =>
            {
                var runner = new Runner();
                await runner.Run(arguments);
            }).GetAwaiter().GetResult();

        }

        class Runner
        {
            public async Task Run(Arguments arguments)
            {
                try
                {
                    var configuration = new ScrapeConfiguration();
                    var configurationJson = File.ReadAllText(arguments.ConfigurationPath ?? "WebScrape.json");
                    configuration.load(configurationJson);

                    var rawPath = arguments.Path ?? configuration.Path;
                    var scraper = new Scraper(configuration, new FileService(), new HttpService());

                    var scraped = new List<ResultScraped>();
                    
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
}

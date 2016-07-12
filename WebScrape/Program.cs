using System;
using System.Text.RegularExpressions;
using System.Web;
using WebScrape.Core;

namespace WebScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = new Parameters(args);
            if (args.Length <= 2)
            {
                Console.WriteLine(parameters.Help);
                return;
            }

            var scraper = new Scraper(parameters, new FileService(), new HttpService());
            var scraped = scraper.Scrape();
            var writer = new Writer(Console.Out);
            writer.Write(scraped, parameters.FieldDelimiter);
        }
    }
}

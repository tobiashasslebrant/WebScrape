using System.IO;
using System.Reflection;

namespace WebScrape
{
    public class Arguments
    {
        public string HelpText
            => "\r\n WEBSCRAPE [/help] [/settings filepath] [/path filepath]" +
               "\r\n   [/help]                              Show this help" +
               "\r\n   [/helpconfiguration]                 Show help about configuration file" +
               "\r\n   [/example]                           Show example of configuration file" +
               "\r\n   [/configurationPath filepath]        File path to configuration file. Defaults to Webscrape.json" +
               "\r\n   [/path urlPath]                      Overrides path in Webscrape.json" +
               "\r\n" ;

        public string HelpConfigurationText
                  => "\r\n _____ Explanation of WebScrape.json __________________________________________________________________" +
                     "\r\n   path                                 Url path to page being scraped." +
                     "\r\n                                         Can enumerate number of calls by using {from.[step].to} syntax" +
                     "\r\n                                         for example: http://test/?page={1.2.5} will make calls to " +
                     "\r\n                                         http://test/?page=1" +
                     "\r\n                                         http://test/?page=3,  " +
                     "\r\n                                         http://test/?page=5" +
                     "\r\n   useCache                             If true serialize all pages being scraped to disk cache." +
                     "\r\n                                         The cache will be used if scraping is used on same page again." +
                     "\r\n   useAsync                             Each request is executed in parallell instead of serial" +
                     "\r\n   requestDelay                         Delay in milliseconds between request" +
                     "\r\n   crawling.itemsParser.parser          <see below>" +
                     "\r\n   crawling.itemsParser.identifier      Identifier used for identify result items" +
                     "\r\n   crawling.followItemLink              If true will follow itemlinks identified by crawling.itemsParser" +
                     "\r\n                                         If false will crawl items on result items page" +
                     "\r\n   crawling.itemLinkParser.parser       <see below>" +
                     "\r\n   crawling.itemLinkParser.identifier   Identifier used for identify item links" +
                     "\r\n   outFormat.fieldDelimiter             Delimiter used to seperate fields" +
                     "\r\n   outFormat.fieldParsers.parser        <see below>" +
                     "\r\n   outFormat.fieldParsers.identifier    Identifier used for identify output field" +
                     "\r\n" +
                     "\r\n _____ Parsers ________________________________________________________________________________________" +
                     "\r\n   css                                  Use css syntax in identifier field" +
                     "\r\n   xpath                                Use xpath syntax in identifier field" +
                     "\r\n   regex                                Use regex syntax in identifier field" +
                     "\r\n";

        public string ExampleText
            => "\r\n _____ Example of WebScrape.json ____________________________________________________________________________" +
               "\r\n" +
               "\r\n" + ExampleFile() +
               "\r\n";

        public Arguments(string[] args)
        {
            for (var index = 0; index < args.Length; ++index)
            {
                if (args[index] == "/configurationPath")
                    ConfigurationPath = args[++index];
                if (args[index] == "/path")
                    Path = args[++index];
                if (args[index] == "/helpconfiguration")
                    ShowHelpConfiguration = true;
                if (args[index] == "/example")
                    ShowExample = true;
                if (args[index] == "/help")
                    ShowHelp = true;
                if (args[index] == "/?")
                    ShowHelp = true;
                if (args[index] == "--help")
                    ShowHelp = true;
                if (args[index] == "-H")
                    ShowHelp = true;
            }
        }

        public string ConfigurationPath { get; }
        public string Path { get; }
        public bool ShowHelp { get; }
        public bool ShowHelpConfiguration { get; }
        public bool ShowExample { get; }

        static string ExampleFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "WebScrape.WebScrape_hemnet.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
            
        }
    }
}
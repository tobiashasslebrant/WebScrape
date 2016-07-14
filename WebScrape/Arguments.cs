namespace WebScrape
{
    public class Arguments
    {
        public string HelpText 
            => "\r\n" +
               "\r\n" +
               "\r\n WEBSCRAPE [/help] [/settings filepath] [/path filepath]" +
               "\r\n   [/help]                              Show this help" +
               "\r\n   [/configurationPath filepath]        File path to json configuration file. Defaults to Webscrape.json" +
               "\r\n   [/path urlPath]                      Url path to page being scraped. Overrides path in Webscrape.json" +
               "\r\n" +
               "\r\n ================== Explanation of WebScrape.json ==================" +
               "\r\n" + 
               "\r\n   path                                 Url path to page being scraped." +
               "\r\n   useCache                             If true serialize all pages being scraped to disk cache." +
               "\r\n   useAsync                             Each request is executed in parallell instead of serial" +
               "\r\n   requestDelay                         Delay in milliseconds between request. Is not used if async" +
               "\r\n                                        The cache will be used if scraping is used on same page again." +
               "\r\n   crawling.itemsParser.parser          <see below>" +
               "\r\n   crawling.itemsParser.identifier      Identifier used for identify result items" +
               "\r\n   crawling.followItemLink              If true will follow itemlinks identified by crawling.itemLinkIdentifier" +
               "\r\n                                        If false will use items in result items" +
               "\r\n   crawling.itemLinkParser.parser       <see below>" +
               "\r\n   crawling.itemLinkParser.identifier   Identifier used for identify item links" +
               "\r\n   outFormat.fieldDelimiter             Delimiter used to seperate fields" +
               "\r\n   outFormat.fieldParsers.parser        <see below>" +
               "\r\n   outFormat.fieldParsers.identifier    Identifier used for identify output field" +
               "\r\n" +
               "\r\n ================== Explanation of Parsers =========================" +
               "\r\n There are several available parsers" +
               "\r\n   css                                  Use css syntax in identifier field" +
               "\r\n   xpath                                Use xpath syntax in identifier field" +
               "\r\n   regex                                Use regex syntax in identifier field" +
               "\r\n" +
               "\r\n";

        public Arguments(string[] args)
        {
            for (var index = 0; index < args.Length; ++index)
            {
                if (args[index] == "/configurationPath")
                    ConfigurationPath = args[++index];
                if (args[index] == "/path")
                    Path = args[++index];
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


    }
}
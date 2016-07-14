namespace WebScrape
{
    public class Arguments
    {
        public string HelpText 
            => "\r\n" +
               "\r\n" +
               "\r\n WEBSCRAPE [/help] [/settings filepath] [/path filepath]" +
               "\r\n   [/help]                              show this help" +
               "\r\n   [/configurationPath filepath]        file path to json settings file. Defaults to Webscrape.json" +
               "\r\n   [/path urlPath]                      url path to page being scraped. Overrides path in Webscrape.json" +
               "\r\n" +
               "\r\n ================== Explanation of WebScrape.json ==================" +
               "\r\n   path                                 url path to page being scraped." +
               "\r\n   useCache                             if true serialize all pages being scraped to disk cache." +
               "\r\n                                        The cache will be used if scraping is used on same page again." +
               "\r\n   crawling.itemsParser.parser          <see below>" +
               "\r\n   crawling.itemsParser.identifier      identifier used for identify result items" +
               "\r\n   crawling.followItemLink              if true will follow itemlinks identified by crawling.itemLinkIdentifier" +
               "\r\n                                        if false will use items in result items" +
               "\r\n   crawling.itemLinkParser.parser       <see below>" +
               "\r\n   crawling.itemLinkParser.identifier   identifier used for identify item links" +
               "\r\n   outFormat.fieldDelimiter             delimiter used to seperate fields" +
               "\r\n   outFormat.fieldParsers.parser        <see below>" +
               "\r\n   outFormat.fieldParsers.identifier    identifier used for identify output field" +
               "\r\n" +
               "\r\n ================== Explanation of Parsers =========================" +
               "\r\n There are several available parsers" +
               "\r\n   css                                  use css syntax in identifier field" +
               "\r\n   xpath                                use xpath syntax in identifier field" +
               "\r\n   regex                                use regex syntax in identifier field" +
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
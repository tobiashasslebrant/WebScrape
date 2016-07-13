namespace WebScrape
{
    public class Arguments
    {
        public string HelpText 
            => "\r\n" +
               "\r\n" +
               "\r\n WEBSCRAPE [/help] [/settings filepath] [/path filepath]" +
               "\r\n   [/help]                 show this help" +
               "\r\n   [/settings filepath]    file path to json settings file. Defaults to Webscrape.json" +
               "\r\n   [/path urlPath]         url path to page being scraped. Overrides path in Webscrape.json" +
               "\r\n" +
               "\r\n === Explanation of WebScrape.json ===" +
               "\r\n   path:                   url path to page being scraped." +
               "\r\n   useCache:               if true serialize all pages being scraped to disk cache." +
               "\r\n                           The cache will be used if scraping is used on same page again." +
               "\r\n   crawling.itemsIdentifier.parser:         <see below>" +
               "\r\n   crawling.itemsIdentifier.identifier:     identifier used for identify result items" +
               "\r\n   crawling.followItemLink:                 if true will follow itemlinks identified by " +
               "\r\n                                            crawling.itemLinkIdentifier." +
               "\r\n                                            if false will use items in result items" +
               "\r\n   crawling.itemLinkIdentifier.parser:      <see below>" +
               "\r\n   crawling.itemLinkIdentifier.identifier:  identifier used for identify item links" +
               "\r\n   outFormat.fieldDelimiter:                delimiter used to seperate fields" +
               "\r\n   outFormat.resultIdentifiers.parser:      <see below>" +
               "\r\n   outFormat.resultIdentifiers.identifier:  identifier used for identify output field" +
               "\r\n" +
               "\r\n === Explanation of Parsers ===" +
               "\r\n There are several available parsers" +
               "\r\n   css                     trying to get values using css syntax in identifier field" +
               "\r\n   xpath                   trying to get values using xpath syntax in identifier field" +
               "\r\n   regex                   trying to get values using regex syntax in identifier field" +
               "\r\n" +
               "\r\n";

        public Arguments(string[] args)
        {
            for (var index = 0; index < args.Length; ++index)
            {
                if (args[index] == "/settings")
                    Settings = args[++index];
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

        public string Settings { get; } = "WebScrape.json";
        public string Path { get; }
        public bool ShowHelp { get; }


    }
}
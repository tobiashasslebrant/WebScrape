using System.Collections.Generic;

namespace WebScrape.Core
{
    public class Parameters
    {
        public string Help => "\r\nWEBSCRAPE path itemSelector [/il itemLinkSelector] [/fd fieldDelimiter] fieldSelectors" +
                              "\r\n  path                       url (or file) to listingpage being scraped" +
                              "\r\n  itemSelector               css selector for identify each item on listingpage" +
                              "\r\n  [/writetodisk]             writes results from web to disk cache" +
                              "\r\n  [/readfromdisk]            reads from disk cache instead of accessing web" +
                              "\r\n  [/il itemLinkSelector]     css selector for identify pagelink on each item. If not specified scraping will" +
                              "\r\n                             try to scrape data from listingpage" +
                              "\r\n  [/fd fieldDelimiter]       character(s) used for delimit fields. Defaults to ;" +
                              "\r\n  fieldSelectors [ ...]      css selectors for each field in outputformat";

        public string Path { get; }
        public string ItemCss { get; }
        public string ItemLinkCss { get; }
        public string FieldDelimiter { get; } = ";";
        public List<string> DataCsses { get; } = new List<string>();
        public bool IsHttp => Path?.ToLower().StartsWith("http") ?? false;
        public bool WriteToDisk { get; }
        public bool ReadFromDisk { get; }

        public Parameters(string[] args)
        {
            if (args.Length < 2) return;

            Path = args[0];
            ItemCss = args[1];

            for (var index = 2; index < args.Length; index++)
                switch (args[index])
                {
                    case "/il": ItemLinkCss = args[++index];
                        break;
                    case "/fd": FieldDelimiter = args[++index];
                        break;
                    case "/writetodisk": WriteToDisk = true;
                        break;
                    case "/readfromdisk": ReadFromDisk = true;
                        break;
                    default: DataCsses.Add(args[index]);
                        break;
                }
        }
    }
}
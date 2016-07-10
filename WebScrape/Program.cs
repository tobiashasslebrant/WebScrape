using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web;
using CsQuery;

namespace WebScrape
{
    class Parameters
    {
        public string Help => "WEBSCRAPE path itemSelector [/il itemLinkSelector] [/fd fieldDelimiter] fieldSelectors" +
                          "\r\n  path                       url (or file) to listingpage being scraped" +
                          "\r\n  itemSelector               css selector for identify each item on listingpage" +
                          "\r\n  [/il itemLinkSelector]     css selector for identify pagelink on each item. If not specified scraping will" +
                          "\r\n                             try to scrape data from listingpage" +
                          "\r\n  [/fd fieldDelimiter]       character(s) used for delimit fields. Defaults to ;" +
                          "\r\n  fieldSelectors [ ...]      css selectors for each field in outputformat";

        public string Path { get; }
        public string ItemCss { get; }
        public string ItemLinkCss { get; }
        public string FieldDelimiter { get; } = ";";
        public List<string> DataCsses { get; } = new List<string>();

        public Parameters(string[] args)
        {
            if (args.Length == 0) return;

            Path = args[0];
            ItemCss = args[1];

            for (var index = 2; index < args.Length; index++)
                switch (args[index])
                {
                    case "/il": ItemLinkCss = args[++index];
                        break;
                    case "/fd": FieldDelimiter = args[++index];
                        break;
                    default: DataCsses.Add(args[index]);
                        break;
                }
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            //args = new[]
            //{
            //    "../../TestData/SearchResult.html",
            //    "#search-results li",
            //    "/il",
            //    "a.item-link-container",
            //    "/fd",
            //    ";",
            //    ".price",
            //    ".city"
            //};

            var paramaters = new Parameters(args);
            if (args.Length == 0)
            {
                Console.WriteLine(paramaters.Help);
                return;
            }
            
            string html;
            if (paramaters.Path.StartsWith("http"))
            {
                var client = new HttpClient();
                html = client.GetStringAsync(paramaters.Path).Result;
            }
            else
                html = File.ReadAllText(Path.GetFullPath(paramaters.Path));

            CQ dom = html;

            var items = dom[paramaters.ItemCss];
            

            foreach (var item in items)
            {
                CQ itemDom = item.InnerHTML;
                var itemLink = itemDom[paramaters.ItemLinkCss].Attr("href");

                if(itemLink == null) continue;

                foreach (var dataCss in paramaters.DataCsses)
                {
                    WriteCsvFormat(itemDom[dataCss].Html(), paramaters.FieldDelimiter);
                    Console.Write(paramaters.FieldDelimiter);
                }

                Console.WriteLine();

            }
        }

        static void WriteCsvFormat(string msg, string fieldDelimiter)
        {
            var text = msg.Trim().Replace(Environment.NewLine, "");
            text = HttpUtility.HtmlDecode(text);
            
            if (text.Contains("\""))
                text = text.Replace("\"", "\"\"");

            if (text.Contains(fieldDelimiter))
                text = $"\"{text}\"";

            Console.Write(text);
        }
    }
}

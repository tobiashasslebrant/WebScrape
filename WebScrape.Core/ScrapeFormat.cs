using System.Collections.Generic;
using CsQuery.Engine;
using Newtonsoft.Json;
using WebScrape.Core.HtmlParsers;

namespace WebScrape.Core
{
    public class ScrapeItem
    {
        public IHtmlParser Parser;
        public string Identifier;
    }

    public class ScrapeFormat
    {
        public ScrapeFormat(string json)
        {
            var settings = JsonConvert.DeserializeObject<dynamic>(json);
            Path = settings.path;
            WriteToDisk = settings.writeToDisk;
            ReadFromDisk = settings.readFromDisk;

            //crawling
            ItemsIdentifier = GetScrapeItem(settings.crawling.itemsIdentifier);
            FollowItemLink = settings.crawling.followItemLink;
            ItemLinkIdentifier = GetScrapeItem(settings.crawling.resultItemLinkIdentifier);

            //outformat
            FieldDelimiter = settings.outFormat.fieldDelimiter;
            var list = new List<ScrapeItem>();
            foreach (dynamic field in settings.outFormat.resultIdentifiers)
                list.Add(GetScrapeItem(field));

            ResultItemsIdentifiers = list;

        }

        public string Path { get; set; }
        public ScrapeItem ItemsIdentifier { get; }
        public IEnumerable<ScrapeItem> ResultItemsIdentifiers { get; }
        public ScrapeItem ItemLinkIdentifier { get; }
        public bool FollowItemLink { get; }
        public string FieldDelimiter { get; }
        public bool WriteToDisk { get; }
        public bool ReadFromDisk { get; }

        ScrapeItem GetScrapeItem(dynamic field)
        {
            return new ScrapeItem
            {
                Identifier = field?.identifier,
                Parser = SelectParser((string)field?.parser)
            };
        }

        IHtmlParser SelectParser(string identifier)
        {
            switch (identifier)
            {
                case "css": return new CssParser();
                case "rexeg": return new RegexParser();
                case "xpath": return new XPathParser();
                default: return new CssParser();
            }
        }
    }
}
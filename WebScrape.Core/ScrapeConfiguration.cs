using System.Collections.Generic;
using Newtonsoft.Json;
using WebScrape.Core.HtmlParsers;

namespace WebScrape.Core
{
    public class ScrapeConfiguration
    {
        
        public void load(string json)
        {
            var settings = JsonConvert.DeserializeObject<dynamic>(json);
            Path = settings.path;
            UseCache = settings.useCache;
        
            //crawling
            ItemsParser = GetScrapeItem(settings.crawling.itemsParser);
            FollowItemLink = settings.crawling.followItemLink;
            ItemLinkParser = GetScrapeItem(settings.crawling.itemLinkParser);

            //outformat
            FieldDelimiter = settings.outFormat.fieldDelimiter;
            var list = new List<HtmlFinder>();
            foreach (dynamic field in settings.outFormat.fieldParsers)
                list.Add(GetScrapeItem(field));

            FieldParsers = list;

        }

        public string Path { get; set; }
        public IHtmlFinder ItemsParser { get; set; }
        public IHtmlFinder ItemLinkParser { get; set; }
        public IEnumerable<IHtmlFinder> FieldParsers { get; set; }
        public bool FollowItemLink { get; set; }
        public string FieldDelimiter { get; set; }
        public bool UseCache { get; set; }

        IHtmlFinder GetScrapeItem(dynamic field)
        {
            IHtmlParser parser = SelectParser((string) field?.parser);
            string identifier = field?.identifier;
            return new HtmlFinder(parser, identifier);
        }

        IHtmlParser SelectParser(string identifier)
        {
            switch (identifier)
            {
                case "css":
                    return new CssParser();
                case "regex":
                    return new RegexParser();
                case "xpath":
                    return new XPathParser();
                default:
                    return new CssParser();
            }
        }
    }
}
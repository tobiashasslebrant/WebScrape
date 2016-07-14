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
            UseAsync = settings.useAsync;
            RequestDelay = settings.requestDelay;
        
            //crawling
            ItemsParser = GetScrapeItem(settings.crawling.itemsParser);
            FollowItemLink = settings.crawling.followItemLink;
            ItemLinkParser = GetScrapeItem(settings.crawling.itemLinkParser);

            //outformat
            FieldDelimiter = settings.outFormat.fieldDelimiter;
            var list = new List<HtmlParserDecorator>();
            foreach (dynamic field in settings.outFormat.fieldParsers)
                list.Add(GetScrapeItem(field));

            FieldParsers = list;

        }

        public int RequestDelay { get; set; }
        public bool UseAsync { get; set; }
        public string Path { get; set; }
        public IHtmlParserDecorator ItemsParser { get; set; }
        public IHtmlParserDecorator ItemLinkParser { get; set; }
        public IEnumerable<IHtmlParserDecorator> FieldParsers { get; set; }
        public bool FollowItemLink { get; set; }
        public string FieldDelimiter { get; set; }
        public bool UseCache { get; set; }

        IHtmlParserDecorator GetScrapeItem(dynamic field)
        {
            IHtmlParser parser = SelectParser((string) field?.parser);
            string identifier = field?.identifier;
            return new HtmlParserDecorator(parser, identifier);
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
using System.Collections.Generic;
using System.Linq;

namespace WebScrape.Core.HtmlParsers
{
    public class XPathParser : IHtmlParser
    {
        public string GetElement(string identifier, string text)
        {
            var doc = new HtmlAgilityPack.HtmlDocument
            {
                OptionFixNestedTags = true,
                OptionAutoCloseOnEnd = true
            };
            doc.LoadHtml(text);
            return doc.DocumentNode.SelectSingleNode(identifier)?.InnerHtml ?? string.Empty;
        }

        public IEnumerable<string> GetElements(string identifier, string text)
        {
            var doc = new HtmlAgilityPack.HtmlDocument
            {
                OptionFixNestedTags = true,
                OptionAutoCloseOnEnd = true
            };
            doc.LoadHtml(text);
            return doc.DocumentNode.SelectNodes(identifier)
                .Select(s => s?.InnerHtml ?? string.Empty);
        }

        public string GetAttr(string identifier, string attribute, string text)
        {
            var doc = new HtmlAgilityPack.HtmlDocument
            {
                OptionFixNestedTags = true,
                OptionAutoCloseOnEnd = true
            };
            doc.LoadHtml(text);
            return doc.DocumentNode.SelectSingleNode(identifier)
                .Attributes[attribute]?.Value ?? string.Empty;
        }
    }
}
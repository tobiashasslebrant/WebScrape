using System.Collections.Generic;
using System.Linq;
using CsQuery;

namespace WebScrape.Core.HtmlParsers
{
    public class CssParser : IHtmlParser
    {
        public string GetElement(string identifier, string text)
        {
            CQ itemDom = text;
            return itemDom[identifier].Html();
        }

        public IEnumerable<string> GetElements(string identifier, string text)
        {
            CQ dom = text;
            var items = dom[identifier];
            foreach (var item in items)
                yield return item.InnerHTML;
        }

        public string GetAttr(string identifier, string attribute,  string text)
        {
            CQ dom = text;
            return dom[identifier].Attr(attribute);
        }
    }

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
           return doc.DocumentNode.SelectSingleNode(identifier).InnerHtml;
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
                .Select(s => s.InnerHtml);
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
                .Attributes[attribute].Value;
        }
    }
}
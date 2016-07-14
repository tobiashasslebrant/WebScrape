using System.Collections.Generic;
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
}
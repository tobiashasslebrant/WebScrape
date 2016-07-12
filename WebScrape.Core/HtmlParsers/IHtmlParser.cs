using System.Collections.Generic;

namespace WebScrape.Core.HtmlParsers
{
    public interface IHtmlParser
    {
        string GetElement(string identifier, string text);
        IEnumerable<string> GetElements(string identifier, string text);
        string GetAttr(string identifier, string attribute, string text);
    }
}
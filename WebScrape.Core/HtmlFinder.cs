using System.Collections.Generic;
using WebScrape.Core.HtmlParsers;

namespace WebScrape.Core
{
    public interface IHtmlFinder
    {
        string Attr(string attribute, string text);
        string Element(string text);
        IEnumerable<string> Elements(string text);
    }

    public class HtmlFinder : IHtmlFinder
    {
        readonly IHtmlParser _htmlParser;
        readonly string _identifier;

        public HtmlFinder(IHtmlParser htmlParser, string identifier)
        {
            _htmlParser = htmlParser;
            _identifier = identifier;
        }

        public string Attr(string attribute, string text) 
            => _htmlParser.GetAttr(_identifier, attribute, text);

        public string Element(string text)
            => _htmlParser.GetElement(_identifier, text);

        public IEnumerable<string> Elements(string text)
            => _htmlParser.GetElements(_identifier, text);
    }
}
using System.Collections.Generic;

namespace WebScrape.Core.HtmlParsers
{
    public interface IHtmlParserDecorator
    {
        string Attr(string attribute, string text);
        string Element(string text);
        IEnumerable<string> Elements(string text);
    }

    public class HtmlParserDecorator : IHtmlParserDecorator
    {
        readonly IHtmlParser _htmlParser;
        readonly string _identifier;

        public HtmlParserDecorator(IHtmlParser htmlParser, string identifier)
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
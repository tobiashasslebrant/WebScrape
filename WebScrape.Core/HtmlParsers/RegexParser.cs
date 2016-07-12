using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebScrape.Core.HtmlParsers
{
    public class RegexParser : IHtmlParser
    {
        public string GetElement(string identifier, string text)
        {
            var regex = new Regex(identifier, RegexOptions.Compiled);
            return regex.Match(text).Value;
        }

        public IEnumerable<string> GetElements(string identifier, string text)
        {
            var regex = new Regex(identifier, RegexOptions.Compiled);
            foreach (Match match in regex.Matches(text))
                yield return match.Value;
        }

        public string GetAttr(string identifier, string attribute, string text)
        {
            var regex = new Regex(identifier, RegexOptions.Compiled);
            return regex.Match(text).Value;
        }
    }
}
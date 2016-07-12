using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace WebScrape.Core
{
    public class Writer
    {
        private readonly TextWriter _textWriter;

        public Writer(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void Write(Scraped scraped, string fieldDelimiter)
        {
            
            foreach (var line in scraped.Items)
            {
                foreach (var field in line)
                    _textWriter.Write(Format(field, fieldDelimiter));
                _textWriter.WriteLine();
            }
        }

        static readonly Regex NoLine = new Regex(@"\r\n?|\n", RegexOptions.Compiled);

        static string Format(string msg, string fieldDelimiter)
        {
            var text = NoLine.Replace(msg, "");

            text = HttpUtility.HtmlDecode(text);

            if (text.Contains("\""))
                text = text.Replace("\"", "\"\"");

            if (text.Contains(fieldDelimiter))
                text = $"\"{text}\"";

            return text;
        }
    }
}

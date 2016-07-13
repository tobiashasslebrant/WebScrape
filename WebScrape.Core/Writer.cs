using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using WebScrape.Core.Models;

namespace WebScrape.Core
{
    public class Writer
    {
        private readonly TextWriter _textWriter;

        public Writer(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void Write(ResultScraped resultScraped, string fieldDelimiter)
        {
            
            foreach (var line in resultScraped.Items)
            {
                foreach (var field in line)
                    _textWriter.Write(Format(field, fieldDelimiter) + fieldDelimiter);
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

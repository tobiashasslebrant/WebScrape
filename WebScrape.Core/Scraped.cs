using System.Collections.Generic;
using System.Linq;

namespace WebScrape.Core
{
    public class Scraped
    {
        public List<List<string>> Items { get; } = new List<List<string>>();
        public void NewItem() => Items.Add(new List<string>());
        public void AddValue(string value) => Items.Last().Add(value);

    }
}
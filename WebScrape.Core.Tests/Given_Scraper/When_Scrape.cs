using System.IO;
using System.Linq;
using NUnit.Framework;

namespace WebScrape.Core.Tests.Given_Scraper
{
    public class When_Scrape : Arrange
    {
        Scraped _result;

        protected override string[] Arguments
            => new[]
            {
                "http://www.hemnet.se/salda/bostader?&page=1",
                "#search-results li",
                "/readfromdisk",
                ".sold-date",
                ".price"
            };

        [SetUp]
        public void Because_of() 
            => _result = Subject.Scrape();

        [Test]
        public void Should_have_more_than_one_item_with_values()
           => Assert.That(_result.Items.Count(w => w[0] != ""), Is.GreaterThan(1));

        [Test]
        public void toby()
        {
            using (var textWriter = File.CreateText("c:\\temp\\toby.txt"))
                new Writer(textWriter).Write(_result,";");


        }
        

    }
}

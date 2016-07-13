using System.IO;
using System.Linq;
using NUnit.Framework;
using WebScrape.Core.Models;

namespace WebScrape.Core.Tests.Given_Scraper
{
    public class When_Scrape : Arrange
    {
        ResultScraped _result;

        protected override object Settings
            => new
            {
                path = "http://www.hemnet.se/salda/bostader?&page=1",
                writeToDisk = false,
                readFromDisk = true,
                crawling = new
                {
                    itemsIdentifier = new
                    {
                        parser = "css",
                        identifier = "#search-results li"
                    },
                    followItemLink = false,
                    itemLinkIdentifier = new
                    {
                        parser = "css",
                        identifier = "li a"
                    }
                },
                outFormat = new
                {
                    fieldDelimiter = ";",
                    resultIdentifiers = new[]
                    {
                        new
                        {
                            parser = "css",
                            identifier = ".sold-date"
                        },
                        new
                        {
                            parser = "css",
                            identifier = ".price"
                        }
                    }
                },
            };
  
        [SetUp]
        public void Because_of() 
            => _result = Subject.Scrape();

        [Ignore("only for integrationtests")]
        [Test]
        public void Should_have_more_than_one_item_with_values()
           => Assert.That(_result.Items.Count(w => w[0] != ""), Is.GreaterThan(1));

        [Ignore("only for integrationtests")]
        [Test]
        public void toby()
        {
            using (var textWriter = File.CreateText("c:\\temp\\toby.txt"))
                new Writer(textWriter).Write(_result,";");
        }
    }
}

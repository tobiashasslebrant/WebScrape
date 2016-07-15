using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using WebScrape.Core.HtmlParsers;
using WebScrape.Core.Models;

namespace WebScrape.Core.Tests.Given_Scraper
{
    public class When_Scrape : Arrange
    {
        ResultScraped _result;
        protected override IEnumerable<string> _items => new[]
        {
            "<li>" +
            "   <span class=\"name\">husby</span>" +
            "   <span class=\"price\">10 kr</span>" +
            "</li>",
            "<li>" +
            "   <span class=\"name\">kista</span>" +
            "   <span class=\"price\">20 kr</span>" +
            "</li>",
        };

        protected override IEnumerable<IHtmlParserDecorator> _fields 
            => new []
            {
                A.Fake<IHtmlParserDecorator>(),
                A.Fake<IHtmlParserDecorator>(),
                A.Fake<IHtmlParserDecorator>()
            };

        [SetUp]
        public void Because_of() 
            => _result = Subject.ScrapeAsync("http://test").Result;

        [Test]
        public void Should_have_two_rows()
            => Assert.That(_result.Items.Count, Is.EqualTo(2));

        [Test]
        public void Should_have_three_fields()
            => Assert.That(_result.Items[0].Count, Is.EqualTo(3));

        [Test]
        public void Should_not_try_to_serialize_cache()
            => A.CallTo(() => _fileService.WriteAsync(A<string>._, A<string>._))
                .MustNotHaveHappened();

        [Test]
        public void Should_get_items_from_http()
          => A.CallTo(() => _httpService.GetStringAsync("http://test"))
              .MustHaveHappened(Repeated.Exactly.Once);

        [Test]
        public void Should_not_try_to_follow_item_links()
            => A.CallTo(() => _httpService.GetStringAsync(A<string>.That.Not.Matches(t => t == "http://test" )))
                .MustNotHaveHappened();

    }
}

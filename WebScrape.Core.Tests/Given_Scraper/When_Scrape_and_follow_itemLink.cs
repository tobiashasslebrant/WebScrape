using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using WebScrape.Core.HtmlParsers;

namespace WebScrape.Core.Tests.Given_Scraper
{
    public class When_Scrape_and_follow_itemLink : Arrange
    {
        protected override IEnumerable<string> _items => new[]
        {
            "<li></li>",
            "<li></li>"
        };
        protected override IEnumerable<IHtmlParserDecorator> _fields => new[] { A.Fake<IHtmlParserDecorator>() };
        protected override bool _followItemLink => true;


        [SetUp]
        public void Because_of()
        {
            var result = Subject.ScrapeAsync("http://test").Result;
        }

        [Test]
        public void Should_get_item_from_link_two_times_because_of_two_items()
            => A.CallTo(() => _httpService.GetStringAsync("http://itemlink"))
                .MustHaveHappened(Repeated.Exactly.Times(2));

    }
}
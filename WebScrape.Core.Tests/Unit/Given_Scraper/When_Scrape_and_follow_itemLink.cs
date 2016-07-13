using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;

namespace WebScrape.Core.Tests.Unit.Given_Scraper
{
    public class When_Scrape_and_follow_itemLink : Arrange
    {
        protected override IEnumerable<string> _items => new[]
        {
            "<li></li>",
            "<li></li>"
        };
        protected override IEnumerable<IHtmlFinder> _fields => new[] { A.Fake<IHtmlFinder>() };
        protected override bool _followItemLink => true;


        [SetUp]
        public void Because_of() 
            => Subject.Scrape();

        [Test]
        public void Should_get_item_from_link_two_times_because_of_two_items()
            => A.CallTo(() => _httpService.GetStringAsync("http://itemlink"))
                .MustHaveHappened(Repeated.Exactly.Times(2));

    }
}
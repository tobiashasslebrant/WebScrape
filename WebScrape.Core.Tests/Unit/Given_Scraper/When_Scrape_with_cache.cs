using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using WebScrape.Core.Models;

namespace WebScrape.Core.Tests.Unit.Given_Scraper
{
    public class When_Scrape_with_cache_first_time : Arrange
    {
        protected override IEnumerable<string> _items => new[] {""};
        protected override IEnumerable<IHtmlFinder> _fields => new[] {A.Fake<IHtmlFinder>()};
        protected override bool _useCache => true;

        [SetUp]
        public void Because_of()
        {
           var result = Subject.ScrapeAsync().Result;
        }

        [Test]
        public void Should_try_to_serialize_cache()
            => A.CallTo(() => _fileService.Write(A<string>._, A<string>._))
                .MustHaveHappened(Repeated.Exactly.Once);

        [Test]
        public void Should_get_items_from_http()
            => A.CallTo(() => _httpService.GetStringAsync("http://test"))
                .MustHaveHappened(Repeated.Exactly.Once);

    }
}


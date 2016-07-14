using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using WebScrape.Core.HtmlParsers;

namespace WebScrape.Core.Tests.Given_Scraper
{
    public class When_Scrape_with_cache_when_cache_already_exists : Arrange
    {
        protected override IEnumerable<string> _items => new[] { "" };
        protected override IEnumerable<IHtmlParserDecorator> _fields => new[] { A.Fake<IHtmlParserDecorator>() };
        protected override bool _useCache => true;

        [SetUp]
        public void Because_of()
        {
            A.CallTo(() => _fileService.Exists(A<string>._)).Returns(true);
            var result =  Subject.ScrapeAsync("http://test").Result;
        }
          

        [Test]
        public void Should_not_try_to_serialize_cache()
            => A.CallTo(() => _fileService.Write(A<string>._, A<string>._))
                .MustNotHaveHappened();

        [Test]
        public void Should_not_get_items_from_http()
            => A.CallTo(() => _httpService.GetStringAsync("http://test"))
                .MustNotHaveHappened();

    }
}
using System.Collections.Generic; 
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using WebScrape.Core.HtmlParsers;
using WebScrape.Core.Services;

namespace WebScrape.Core.Tests.Unit.Given_Scraper
{
    public abstract class Arrange
    {
        protected Scraper Subject;
        protected IHttpService _httpService;
        protected IFileService _fileService;
        protected ScrapeConfiguration _configuration;
        protected virtual bool _followItemLink => false;
        protected virtual bool _useCache => false;

        protected virtual IEnumerable<string> _items => Enumerable.Empty<string>();
        protected virtual IEnumerable<IHtmlFinder> _fields => Enumerable.Empty<IHtmlFinder>();

        [SetUp]
        protected void BaseSetup()
        {
            var itemsFinder = A.Fake<IHtmlFinder>();
            var itemLinkFinder = A.Fake<IHtmlFinder>();

            A.CallTo(() => itemsFinder.Elements(A<string>._)).Returns(_items);
            A.CallTo(() => itemLinkFinder.Attr("href",A<string>._)).Returns("http://itemlink");
            
            _configuration = new ScrapeConfiguration
            {
                Path = "http://test",
                ItemsParser = itemsFinder,
                UseCache = _useCache,
                FollowItemLink = _followItemLink,
                ItemLinkParser = itemLinkFinder,
                FieldParsers = _fields
            };

            _fileService = A.Fake<IFileService>();
            _httpService = A.Fake<IHttpService>();
            Subject = new Scraper(_configuration, _fileService, _httpService);

        }

    }
}
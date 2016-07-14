using NUnit.Framework;
using WebScrape.Core.Services;

namespace WebScrape.Core.Tests.Given_Scraper
{
    public abstract class Arrange
    {
        protected Scraper Subject;
        protected abstract object Settings { get; }

        [SetUp]
        protected void BaseSetup()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Settings);
            var configuration = new ScrapeConfiguration();
            configuration.load(json);
            Subject = new Scraper(configuration, new FileService(), new HttpService());

        }

    }
}
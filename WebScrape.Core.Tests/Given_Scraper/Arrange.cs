using NUnit.Framework;

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
             Subject = new Scraper(new ScrapeFormat(json), new FileService(), new HttpService(), new CssParser());

        }

    }
}
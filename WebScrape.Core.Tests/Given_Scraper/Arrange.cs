using NUnit.Framework;

namespace WebScrape.Core.Tests.Given_Scraper
{
    public abstract class Arrange
    {
        protected Scraper Subject;
        protected abstract string[] Arguments { get; }

        [SetUp]
        protected void BaseSetup()
        {
            Subject = new Scraper(new Parameters(Arguments), new FileService(), new HttpService());
        }

    }
}
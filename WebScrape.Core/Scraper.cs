namespace WebScrape.Core
{
    public class Scraper
    {
        private readonly ScrapeFormat _parameters;
        private readonly FileService _fileService;
        private readonly HttpService _httpService;
        private readonly IHtmlParser _htmlParser;

        public Scraper(ScrapeFormat parameters, FileService fileService, HttpService httpService, IHtmlParser htmlParser)
        {
            _parameters = parameters;
            _fileService = fileService;
            _httpService = httpService;
            _htmlParser = htmlParser;
        }

        public Scraped Scrape ()
        {
            var html = _parameters.ReadFromDisk
                ? _fileService.ReadFromDisk(CacheType.Result, _parameters.Path, 0)
                : _httpService.GetStringAsync(_parameters.Path);
                   
            if (_parameters.WriteToDisk)
                _fileService.WriteToDisk(CacheType.Result, _parameters.Path, html, 0);

            var scraped = new Scraped();
            var htmlItems = _parameters.ItemsIdentifier.Parser.GetElements(_parameters.ItemsIdentifier.Identifier,html);
            var index = 0;
            
            foreach (var htmlItem in htmlItems)
            {
                var item = htmlItem;
                if (_parameters.FollowItemLink)
                {
                    var itemLink =_parameters.ItemLinkIdentifier.Parser.GetAttr(
                            _parameters.ItemLinkIdentifier.Identifier, "href", html);

                    if (itemLink == null) continue;

                    item = _parameters.ReadFromDisk 
                        ? _fileService.ReadFromDisk(CacheType.Item, itemLink, index) 
                        : _httpService.GetStringAsync(itemLink);

                   if(_parameters.WriteToDisk)
                        _fileService.WriteToDisk(CacheType.Item,itemLink, item, index);
                }

                scraped.NewItem();
                foreach (var identifier in _parameters.ResultItemsIdentifiers)
                    scraped.AddValue(identifier.Parser.GetElement(identifier.Identifier,item));
                 
                index++;
            }
            return scraped;
        }
       
    }
}
using CsQuery;

namespace WebScrape.Core
{
    public class Scraper
    {
        private readonly Parameters _parameters;
        private readonly FileService _fileService;
        private readonly HttpService _httpService;

        public Scraper(Parameters parameters, FileService fileService, HttpService httpService)
        {
            _parameters = parameters;
            _fileService = fileService;
            _httpService = httpService;
        }

        public Scraped Scrape ()
        {
           var scraped = new Scraped();
            var html = _parameters.ReadFromDisk
                ? _fileService.ReadFromDisk(CacheType.Result, _parameters.Path, 0)
                : _parameters.IsHttp
                    ? _httpService.GetStringAsync(_parameters.Path)
                    : _fileService.ReadAllText(_parameters.Path);

            if (_parameters.WriteToDisk)
                _fileService.WriteToDisk(CacheType.Result, _parameters.Path, html, 0);

            CQ dom = html;

            var items = dom[_parameters.ItemCss];

            var index = 0;
            
            foreach (var item in items)
            {
                if (!item.ChildrenAllowed) continue;

                CQ itemDom = item.InnerHTML;

                scraped.NewItem();

                if (_parameters.ItemLinkCss != null)
                {
                    var itemLink = itemDom[_parameters.ItemLinkCss].Attr("href");
                    if (itemLink == null) continue;

                    var itemHtml = _parameters.ReadFromDisk 
                        ? _fileService.ReadFromDisk(CacheType.Item, itemLink, index) 
                        : _httpService.GetStringAsync(itemLink);

                    itemDom = itemHtml;

                    if(_parameters.WriteToDisk)
                        _fileService.WriteToDisk(CacheType.Item,itemLink,itemHtml,index);
                }
                
                foreach (var dataCss in _parameters.DataCsses)
                    scraped.AddValue(itemDom[dataCss].Html());
            
                index++;
            }
            return scraped;
        }
    }
}
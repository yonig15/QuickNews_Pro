namespace QuickNews.MicroService.VModel
{
    public class AddRssRequest
    {
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public int WebSiteId { get; set; }
    }
}

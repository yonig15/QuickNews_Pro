using QuickNews.Model;

namespace QuickNews.Model
{
	public class M_NewsItem : BaseModel
	{
		public string ItemId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public string ImageUrl { get; set; }
		public DateTime PublishDate { get; set; }
		public int WebSiteId { get; set; }
		public virtual M_WebSite WebSite { get; set; }
		public int CategoryId { get; set; }
		public virtual M_Category Category { get; set; }
		public int ClickCount { get; set; }
	}
}
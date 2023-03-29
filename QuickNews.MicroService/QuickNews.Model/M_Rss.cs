using QuickNews.Model;

namespace QuickNews.Model
{
	public class M_Rss : BaseModel
	{
		public string Url { get; set; }
		public M_Category M_Category { get; set; }
		public M_WebSite M_WebSite { get; set; }
	}
}
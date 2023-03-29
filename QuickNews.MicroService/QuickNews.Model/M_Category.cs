using QuickNews.Model;

namespace QuickNews.Model
{
	public class M_Category : BaseModel
	{
		public string Topic { get; set; }
		public virtual List<M_User> Users { get; set; }
		public virtual List<M_Rss> Rss { get; set; }
	}
}
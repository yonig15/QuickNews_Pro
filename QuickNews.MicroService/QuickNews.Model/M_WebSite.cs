using QuickNews.Model;
using System.Security.Cryptography;

namespace QuickNews.Model
{
	public class M_WebSite : BaseModel
	{
		public string Name { get; set; }
		public virtual List<M_User> Users { get; set; }
		public virtual List<M_Rss> Rss { get; set; }
	}
}
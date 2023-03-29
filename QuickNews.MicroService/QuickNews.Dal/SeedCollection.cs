using QuickNews.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNews.Dal
{
	public partial class DataLayer
	{
		private List<M_WebSite> SeedWebsites()
		{
			return new List<M_WebSite>()
			{
                new M_WebSite {Id = 1, Name = "globes"},
				new M_WebSite {Id = 2, Name = "ynet"},
				new M_WebSite {Id = 3, Name = "maariv"},
				new M_WebSite {Id = 4, Name = "walla"},
			};
		}
		private List<M_Category> SeedCategories()
		{
			return new List<M_Category>()
			{
				new M_Category {Id = 1, Topic = "Car"},
				new M_Category {Id = 2, Topic = "Tourism"},
				new M_Category {Id = 3, Topic = "Technology"},
				new M_Category {Id = 4, Topic = "Israel"},
				new M_Category {Id = 5, Topic = "Global"},
				new M_Category {Id = 6, Topic = "Real Estate"},
				new M_Category {Id = 7, Topic = "Capital Market"},
				new M_Category {Id = 8, Topic = "Career"},
				new M_Category {Id = 9, Topic = "Sport"},
				new M_Category {Id = 10, Topic = "Health"},
				new M_Category {Id = 11, Topic = "Food"},
				new M_Category {Id = 12, Topic = "Economy"},
				new M_Category {Id = 13, Topic = "Science and Nature"},
				new M_Category {Id = 14, Topic = "Computers"},
				new M_Category {Id = 15, Topic = "Business"},
				new M_Category {Id = 16, Topic = "Military"},
				new M_Category {Id = 17, Topic = "Music"},
				new M_Category {Id = 18, Topic = "Fashion"},
				new M_Category {Id = 19, Topic = "Law"},
				new M_Category {Id = 20, Topic = "Parents"},
				new M_Category {Id = 21, Topic = "Games"},
			};
		}
		private List<M_Rss> SeedRSSFeeds()
		{
			var websites = SeedWebsites();
			var categories = SeedCategories();
			return new List<M_Rss>()
			{
                //Globes
                new M_Rss {M_Category = categories[1],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=9010",M_WebSite = websites[0]},
				new M_Rss {M_Category = categories[0],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=3220",M_WebSite = websites[0]},
				new M_Rss {M_Category = categories[2],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=594",M_WebSite = websites[0]},
				new M_Rss {M_Category = categories[3],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=9917",M_WebSite = websites[0]},
				new M_Rss {M_Category = categories[4],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=1225",M_WebSite = websites[0]},
				new M_Rss {M_Category = categories[5],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=607",M_WebSite = websites[0]},
				new M_Rss {M_Category = categories[6],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=829",M_WebSite = websites[0]},
				new M_Rss {M_Category = categories[7],Url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=3266",M_WebSite = websites[0]},
                //Ynet
                new M_Rss {M_Category = categories[8],Url="https://www.ynet.co.il/Integration/StoryRss3.xml",M_WebSite = websites[1]},
				new M_Rss {M_Category = categories[9],Url="https://www.ynet.co.il/Integration/StoryRss1208.xml",M_WebSite = websites[1]},
				new M_Rss {M_Category = categories[10],Url="https://www.ynet.co.il/Integration/StoryRss975.xml",M_WebSite = websites[1]},
				new M_Rss {M_Category = categories[11],Url="https://www.ynet.co.il/Integration/StoryRss6.xml",M_WebSite = websites[1]},
				new M_Rss {M_Category = categories[12],Url="https://www.ynet.co.il/Integration/StoryRss2142.xml",M_WebSite = websites[1]},
				new M_Rss {M_Category = categories[0],Url="https://www.ynet.co.il/Integration/StoryRss550.xml",M_WebSite = websites[1]},
				new M_Rss {M_Category = categories[1],Url="https://www.ynet.co.il/Integration/StoryRss598.xml",M_WebSite = websites[1]},
				new M_Rss {M_Category = categories[13],Url="https://www.ynet.co.il/Integration/StoryRss544.xml",M_WebSite = websites[1]},
                //Maariv
                new M_Rss {M_Category = categories[14],Url="https://www.maariv.co.il/Rss/RssFeedsAsakim",M_WebSite = websites[2]},
				new M_Rss {M_Category = categories[8],Url="https://www.maariv.co.il/Rss/RssFeedsSport",M_WebSite = websites[2]},
				new M_Rss {M_Category = categories[2],Url="https://www.maariv.co.il/Rss/RssFeedsTechnologeya",M_WebSite = websites[2]},
				new M_Rss {M_Category = categories[15],Url="https://www.maariv.co.il/Rss/RssFeedsZavaVeBetachon",M_WebSite = websites[2]},
				new M_Rss {M_Category = categories[16],Url="https://www.maariv.co.il/Rss/RssFeedsMozika",M_WebSite = websites[2]},
				new M_Rss {M_Category = categories[17],Url="https://www.maariv.co.il/Rss/RssFeedsOfna",M_WebSite = websites[2]},
				new M_Rss {M_Category = categories[18],Url="https://www.maariv.co.il/Rss/RssMishpat",M_WebSite = websites[2]},
				new M_Rss {M_Category = categories[19],Url="https://www.maariv.co.il/Rss/RssFeedsNewParents",M_WebSite = websites[2]},
                //Walla
				new M_Rss {M_Category = categories[15],Url="https://rss.walla.co.il/feed/2689",M_WebSite = websites[3]},
				new M_Rss {M_Category = categories[3],Url="https://rss.walla.co.il/feed/1",M_WebSite = websites[3]},
				new M_Rss {M_Category = categories[4],Url="https://rss.walla.co.il/feed/2",M_WebSite = websites[3]},
				new M_Rss {M_Category = categories[17],Url="https://rss.walla.co.il/feed/2101",M_WebSite = websites[3]},
				new M_Rss {M_Category = categories[2],Url="https://rss.walla.co.il/feed/4000",M_WebSite = websites[3]},
				new M_Rss {M_Category = categories[20],Url="https://rss.walla.co.il/feed/2266",M_WebSite = websites[3]},
				new M_Rss {M_Category = categories[1],Url="https://rss.walla.co.il/feed/2500",M_WebSite = websites[3]}
			};
		}
	}
}

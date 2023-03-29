using QuickNews.Dal;
using QuickNews.Model;
using QuickNews.Utilities;
using static QuickNews.Utilities.LogManager;


namespace QuickNews.Entities
{
	public class MainManager
	{
		public LogManager Log;
		public DataLayer db = DataLayer.Data;

		public S_WebSiteService websiteService;
		public S_CategoryService CategoryService;
		public S_RssService rssService;
		public S_UserService userService;
		public S_NewsItemService newsItemService;

		public NewsWebsite websiteYnet;
		public NewsWebsite websiteWalla;
		public NewsWebsite websiteMaariv;
		public NewsWebsite websiteGlobes;

		public List<M_WebSite> webSitesList = new List<M_WebSite>();
		public List<M_Category> categoriesList = new List<M_Category>();
		public List<M_Rss> rssesList = new List<M_Rss>();
		public List<M_User> usersList = new List<M_User>();
		public List<M_NewsItem> newsItemsList = new List<M_NewsItem>();
		//public Queue<M_NewsItem> NewsItems = new Queue<M_NewsItem>();
		//private CancellationTokenSource cts = new CancellationTokenSource();

		private static readonly MainManager _instance = new MainManager();
		public static MainManager Instance { get { return _instance; } }

		private MainManager()
		{
			rssesList = db.RssAllInclude();
			Init();
		}
		public void Init()
		{
			Log = new LogManager(providerType.File);

			websiteService = new S_WebSiteService(Log);
			CategoryService = new S_CategoryService(Log);
			rssService = new S_RssService(Log);
			userService = new S_UserService(Log);
			newsItemService = new S_NewsItemService(Log);

			websiteYnet = new NewsWebsite("Ynet", Log);
			websiteYnet.newsWebsite.Init(rssesList);
			websiteWalla = new NewsWebsite("Walla", Log);
			websiteWalla.newsWebsite.Init(rssesList);
			websiteMaariv = new NewsWebsite("Maariv", Log);
			websiteMaariv.newsWebsite.Init(rssesList);
			websiteGlobes = new NewsWebsite("Globes", Log);
			websiteGlobes.newsWebsite.Init(rssesList);
		}
	}
}
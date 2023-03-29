using Microsoft.Extensions.Configuration;
using QuickNews.Model;
using System.Data.Entity;

namespace QuickNews.Dal
{
	public partial class DataLayer: DbContext
	{
		private DataLayer() : base(GetConfigConnectionString())
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataLayer>());

			try
			{
				if (!WebSites.Any())
				{
					Seed();
				}
			}
			catch (ArgumentNullException)
			{

				throw;
			}
		}

		public static string GetConfigConnectionString()
		{
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
			string connectionString = config.GetConnectionString("QuickNews");
			return connectionString;
		}

		private static DataLayer _data;
		public static DataLayer Data
		{
			get
			{
				if (_data == null)
				{
					_data = new DataLayer();
				}
				return _data;
			}
		}

		private void Seed()
		{
			var rsses = SeedRSSFeeds();
			NewsItems.Add(
				new M_NewsItem
				{
					ItemId = "string",
					Title = "string",
					Description = "string",
					Link = "string",
					ImageUrl = "string",
					PublishDate = DateTime.Now,
					WebSiteId = 1,
					CategoryId = 1,
					ClickCount = 0
				});
			Rsses.AddRange(rsses);

			//שמירת שינויים במסד נתונים
			SaveChanges();
		}

		public List<M_User> UsersAllInclude()
		{
			return Users.Include(u => u.Interests).ToList();
		}

		public List<M_Rss> RssAllInclude()
		{
			return Rsses.Include(r => r.M_WebSite).Include(r => r.M_Category).ToList();
		}

		//DbSet- פקודה ליצירת טבלאות בדטה בייס
		//טבלת משתמשים
		public DbSet<M_User> Users { get; set; }
		public DbSet<M_WebSite> WebSites { get; set; }
		public DbSet<M_Category> Categories { get; set; }
		public DbSet<M_Rss> Rsses { get; set; }
		public DbSet<M_NewsItem> NewsItems { get; set; }
	}



}
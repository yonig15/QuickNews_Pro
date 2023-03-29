using QuickNews.Model;
using QuickNews.Utilities;

namespace QuickNews.Entities
{
    public class S_RssService : BaseEntity
    {
        public S_RssService(LogManager log) : base(log) { }

        public void ClearList()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_RssService \ ClearList  ran Successfully - ");

				MainManager.Instance.rssesList.Clear();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public List<M_Rss> GetAllRsses()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_RssService \ GetAllRsses  ran Successfully - ");

				MainManager.Instance.rssesList = MainManager.Instance.db.Rsses.ToList();
                return MainManager.Instance.rssesList;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public M_Rss GetRssById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_RssService \ GetRssById  ran Successfully - ");

				M_Rss rss = MainManager.Instance.db.Rsses.Find(id);
                return rss;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void AddNewRss(string url, int CategoryId, int websiteId)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_RssService \ AddNewRss  ran Successfully - ");

				M_Rss rss = new M_Rss
                {
                    Url = url,
                    M_Category = Dal.DataLayer.Data.Categories.ToList().Find(c => c.Id == CategoryId),
                    M_WebSite = Dal.DataLayer.Data.WebSites.ToList().Find(w => w.Id == websiteId)
                };
                MainManager.Instance.rssesList.Add(rss);
                MainManager.Instance.db.Rsses.Add(rss);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void UpdateRssById(int id, string url, int CategoryId, int websiteId)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_RssService \ UpdateRssById  ran Successfully - ");

				M_Rss rss = MainManager.Instance.db.Rsses.Find(id);
                if (rss != null)
                {
                    rss.Url = url;
                    rss.M_Category = Dal.DataLayer.Data.Categories.ToList().Find(c => c.Id == CategoryId);
                    rss.M_WebSite = Dal.DataLayer.Data.WebSites.ToList().Find(w => w.Id == websiteId);
					MainManager.Instance.rssesList[id] = rss;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void DeleteRssById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_RssService \ DeleteRssById  ran Successfully - ");

				M_Rss rss = MainManager.Instance.db.Rsses.Find(id);
                if (rss != null)
                {
                    MainManager.Instance.rssesList.Remove(rss);
                    MainManager.Instance.db.Rsses.Remove(rss);
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }
    }
}

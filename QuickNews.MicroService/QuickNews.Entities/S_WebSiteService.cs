using QuickNews.Model;
using QuickNews.Utilities;

namespace QuickNews.Entities
{
    public class S_WebSiteService : BaseEntity
    {
        public S_WebSiteService(LogManager log) : base(log)
        {
        }

        public void ClearList()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_WebSiteService \ ClearList  ran Successfully - ");

				MainManager.Instance.webSitesList.Clear();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
				throw;
            }
        }

        public List<M_WebSite> GetAllWebSites()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_WebSiteService \ GetAllWebSites  ran Successfully - ");

				MainManager.Instance.webSitesList = MainManager.Instance.db.WebSites.ToList();
                return MainManager.Instance.webSitesList;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
				throw;
            }
        }

        public M_WebSite GetWebSiteById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_WebSiteService \ GetWebSiteById  ran Successfully - ");

				M_WebSite webSite = MainManager.Instance.db.WebSites.Find(id);
                return webSite;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void AddNewWebSite(string name)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_WebSiteService \ AddNewWebSite  ran Successfully - ");

				M_WebSite webSite = new M_WebSite
                {
                    Name = name
                };
                MainManager.Instance.webSitesList.Add(webSite);
                MainManager.Instance.db.WebSites.Add(webSite);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void UpdateWebSiteById(int id, string name)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_WebSiteService \ UpdateWebSiteById  ran Successfully - ");

				M_WebSite webSite = MainManager.Instance.db.WebSites.Find(id);
                if (webSite != null)
                {
                    webSite.Name = name;
                    MainManager.Instance.webSitesList[id].Name = name;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void DeleteWebSiteById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_WebSiteService \ DeleteWebSiteById  ran Successfully - ");

				M_WebSite webSite = MainManager.Instance.db.WebSites.Find(id);
                if (webSite != null)
                {
                    MainManager.Instance.webSitesList.Remove(webSite);
                    MainManager.Instance.db.WebSites.Remove(webSite);
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

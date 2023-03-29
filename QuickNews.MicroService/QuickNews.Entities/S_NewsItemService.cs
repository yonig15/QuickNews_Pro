using QuickNews.Model;
using QuickNews.Utilities;


namespace QuickNews.Entities
{
    public class S_NewsItemService : BaseEntity
    {
        public S_NewsItemService(LogManager log) : base(log) { }

        public void ClearList()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ ClearList  ran Successfully - ");
				MainManager.Instance.newsItemsList.Clear();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
            }
        }

        public List<M_NewsItem> GetAllNewsItems()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ GetAllNewsItems  ran Successfully - ");

				MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();
                return MainManager.Instance.newsItemsList;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public List<M_NewsItem> GetAllNewsItemsByTopic(string topic)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ GetAllNewsItemsByTopic  ran Successfully - ");

				MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();
                return MainManager.Instance.newsItemsList.Where(n => n.Category.Topic == topic).ToList();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public List<M_NewsItem> GetTrendingNewsItems(string userId)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ GetTrendingNewsItems  ran Successfully - ");
                var users = MainManager.Instance.db.UsersAllInclude();
				M_User user = users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return null;
                }

                // Get the user's interests as a list of M_Category IDs
                List<int> CategoryIds = user.Interests.Select(i => i.Id).ToList();

                MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();

                // Retrieve the news items that match the user's interests and meet the other criteria
                List<M_NewsItem> newsItems = MainManager.Instance.db.NewsItems
                    .Where(n => CategoryIds.Contains(n.CategoryId) && n.Category.Topic != "BreakingNews" && n.ClickCount > 0)
                    .OrderByDescending(n => n.ClickCount)
                    .Take(10)
                    .ToList();

                return newsItems;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public List<M_NewsItem> GetCuriousNewsItems(string userId)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ GetCuriousNewsItems  ran Successfully - ");

				var users = MainManager.Instance.db.UsersAllInclude();
				M_User user = users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    return null;
                }

                // Get the user's interests as a list of M_Category IDs
                List<int> CategoryIds = user.Interests.Select(i => i.Id).ToList();

                MainManager.Instance.newsItemsList = MainManager.Instance.db.NewsItems.ToList();

                // Retrieve the news items that match the user's interests and meet the other criteria
                List<M_NewsItem> newsItems = MainManager.Instance.db.NewsItems
                    .Where(n => CategoryIds.Contains(n.CategoryId) && n.Category.Topic != "BreakingNews" && n.ClickCount == 0)
                    .Take(10)
                    .ToList();

                return newsItems;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public M_NewsItem GetNewsItemById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ GetNewsItemById  ran Successfully - ");

				M_NewsItem newsItem = MainManager.Instance.db.NewsItems.Find(id);
                return newsItem;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void AddNewNewsItem(string itemId, string title, string description, string link, string imageUrl, DateTime publishDate, int CategoryId, int websiteId)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ AddNewNewsItem  ran Successfully - ");

				M_NewsItem newsItem = new M_NewsItem
                {
                    ItemId = itemId,
                    Title = title,
                    Description = description,
                    Link = link,
                    ImageUrl = imageUrl,
                    ClickCount = 0,
                    PublishDate = publishDate,
                    CategoryId = CategoryId,
                    WebSiteId = websiteId
                };
                MainManager.Instance.newsItemsList.Add(newsItem);
                MainManager.Instance.db.NewsItems.Add(newsItem);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void UpdateNewsItemById(string id, /*string title, string description, string link, string imageUrl, DateTime publishDate,*/ int clickCount)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ UpdateNewsItemById  ran Successfully - ");

				M_NewsItem newsItem = MainManager.Instance.db.NewsItems.FirstOrDefault(i => i.ItemId == id);
                if (newsItem != null)
                {
                    newsItem.ClickCount = clickCount;
                    MainManager.Instance.newsItemsList[MainManager.Instance.newsItemsList.FindIndex(u => u.ItemId == id)] = newsItem;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void DeleteNewsItemById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_NewsItemService \ DeleteNewsItemById  ran Successfully - ");

				M_NewsItem newsItem = MainManager.Instance.db.NewsItems.Find(id);
                if (newsItem != null)
                {
                    MainManager.Instance.newsItemsList.Remove(newsItem);
                    MainManager.Instance.db.NewsItems.Remove(newsItem);
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

using QuickNews.Dal;
using QuickNews.Model;
using QuickNews.Utilities;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace QuickNews.Entities
{
    public class E_Walla : BaseEntity, INewsWebsite
    {
		public E_Walla(LogManager log) : base(log) { }

		public Queue<M_NewsItem> NewsItems { get; set; }
        private CancellationTokenSource cts;
        private List<M_Rss> wallaRsses = new List<M_Rss>();

        public void Init(List<M_Rss> rssesList)
        {
            try
            {
                wallaRsses = rssesList.FindAll(rss => rss.M_WebSite.Id == 4);
                NewsItems = new Queue<M_NewsItem>();
                cts = new CancellationTokenSource();

                Task.Run(() => InsertNewsItem(cts.Token), cts.Token);
                Task.Run(() => CreateNewsItems(cts.Token), cts.Token);

				Log.LogEvent(@"Entities \ E_Walla \ Init  ran Successfully - ");
			}
			catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }
        public async Task InsertNewsItem(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (NewsItems.Count > 0)
                    {
                        var newsItem = NewsItems.Dequeue();
                            DataLayer.Data.NewsItems.Add(newsItem);
							DataLayer.Data.SaveChanges();

							Log.LogEvent(@"Entities \ E_Walla \ InsertNewsItem  ran Successfully - ");
                        //if (!DataLayer.Data.NewsItems.Any(i => i.ItemId == newsItem.ItemId))
                        //{
                        //}
                    }
                    await Task.Delay(100, cancellationToken);
                }
                catch (Exception ex)
                {
                    Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

                    throw;
                }
            }
        }
        public async Task CreateNewsItems(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                        foreach (var rss in wallaRsses)
                        {
                            var webClient = new WebClient();
                            var rssData = await webClient.DownloadStringTaskAsync(rss.Url);
                            var rssXml = XDocument.Parse(rssData);
                            var items = rssXml.Descendants("item");
                            lock (DataLayer.Data.NewsItems)
                            {
                                foreach (var item in items)
                                {
                                    var itemId = Regex.Match(item.Element("guid")?.Value, @"\d+$")?.Value;
                                    if (!DataLayer.Data.NewsItems.ToList().Any(i => i.ItemId == itemId))
                                    {
                                        var descriptionHtml = item.Element("description")?.Value;
                                        var description = Regex.Replace(descriptionHtml, "<.*?>", string.Empty);
                                        var startIndex = description.IndexOf("<br/>") + 1;
                                        var endIndex = description.Length - startIndex;
                                        var hebrewText = description.Substring(startIndex, endIndex).Trim();

                                        var newsItem = new M_NewsItem
                                        {
                                            ItemId = itemId,
                                            Title = item.Element("title")?.Value,
                                            Description = hebrewText,
                                            Link = item.Element("link")?.Value,
                                            ImageUrl = item.Element("enclosure")?.Attribute("url")?.Value,
                                            PublishDate = DateTime.Parse(item.Element("pubDate")?.Value),
                                            WebSiteId = 4,
                                            CategoryId = rss.M_Category.Id,
                                            ClickCount = 0
                                        };

                                        //MainManager.Instance.NewsItems.Enqueue(newsItem);
                                        NewsItems.Enqueue(newsItem);

                                        Log.LogEvent(@"Entities \ E_Walla \ CreateNewsItems  ran Successfully - ");
                                    }
                                }
                            }
                        }
                        await Task.Delay(3600 * 1000, cancellationToken); //every 60 minutes
                }
                catch (Exception ex)
                {
					Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

					throw;
                }
            }
        }
        public void Stop()
        {
			Log.LogEvent(@"Entities \ E_Walla \ Stop  ran Successfully - ");

			cts.Cancel();
        }
    }
}
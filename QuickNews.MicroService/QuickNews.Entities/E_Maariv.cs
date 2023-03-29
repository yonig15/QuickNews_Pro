using QuickNews.Dal;
using QuickNews.Model;
using QuickNews.Utilities;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace QuickNews.Entities
{
    public class E_Maariv : BaseEntity, INewsWebsite
    {
		public E_Maariv(LogManager log) : base(log) { }

		public Queue<M_NewsItem> NewsItems { get; set; }
        private bool cts = false;
        private List<M_Rss> maarivRsses = new List<M_Rss>();

        public void Init(List<M_Rss> rssesList)
        {
            try
            {
                maarivRsses = rssesList.FindAll(rss => rss.M_WebSite.Id == 3);
                NewsItems = new Queue<M_NewsItem>();

                //Task.Run(() => InsertNewsItem(cts));
                Task.Run(() => CreateNewsItems(cts));

				Log.LogEvent(@"Entities \ E_Maariv \ Init  ran Successfully - ");
			}
			catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

       // public async Task InsertNewsItem(bool cancellationToken)
       // {
       //     while (!cancellationToken)
       //     {
       //         try
       //         {
       //             if (NewsItems.Count > 0)
       //             {
       //                 var newsItem = NewsItems.Dequeue();
       //                 if (!DataLayer.Data.NewsItems.Any(i => i.ItemId == newsItem.ItemId))
       //                 {
       //                     DataLayer.Data.NewsItems.Add(newsItem);
							//DataLayer.Data.SaveChanges();

							//Log.LogEvent(@"Entities \ E_Maariv \ InsertNewsItem  ran Successfully - ");
       //                 }
       //             }
       //             await Task.Delay(100);
       //         }
       //         catch (Exception ex)
       //         {
       //             Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

       //             throw;
       //         }
       //     }
       // }

        public async Task CreateNewsItems(bool cancellationToken)
        {
            while (!cancellationToken)
            {
                try
                {
                        foreach (var rss in maarivRsses)
                        {
                            var webClient = new WebClient();
                            var rssData = await webClient.DownloadStringTaskAsync(rss.Url);
                            var rssXml = XDocument.Parse(rssData);
                            var items = rssXml.Descendants("item");

                            lock (DataLayer.Data.NewsItems)
                            {
                                foreach (var item in items)
                                {
                                    var itemId = item.Element("itemID")?.Value;
                                    if (!DataLayer.Data.NewsItems.ToList().Any(i => i.ItemId == itemId))
                                    {
                                        var descriptionHtml = item.Element("description")?.Value;

                                        var description = Regex.Replace(descriptionHtml, "<.*?>", string.Empty);
                                        var startIndex = description.IndexOf("<br/>") + 5;
                                        var endIndex = description.Length - startIndex;
                                        var hebrewText = description.Substring(startIndex, endIndex).Trim();

                                        var newsItem = new M_NewsItem
                                        {

                                            ItemId = itemId,
                                            Title = item.Element("title")?.Value,
                                            Description = hebrewText,
                                            Link = item.Element("link")?.Value,
                                            ImageUrl = Regex.Match(descriptionHtml, @"(?<=src=('|""))[^'""]+(?=('|""))")?.Value,
                                            PublishDate = DateTime.Parse(item.Element("pubDate")?.Value),
                                            WebSiteId = 3,
                                            CategoryId = rss.M_Category.Id,
                                            ClickCount = 0
                                        };

                                        //MainManager.Instance.NewsItems.Enqueue(newsItem);
                                        //NewsItems.Enqueue(newsItem);
									    DataLayer.Data.NewsItems.Add(newsItem);
									    DataLayer.Data.SaveChanges();
									    Log.LogEvent(@"Entities \ E_Maariv \ CreateNewsItems  ran Successfully - ");
                                    }
                                }
                            }
                        }
                        await Task.Delay(3600 * 1000); //every 60 minutes
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
			Log.LogEvent(@"Entities \ E_Maariv \ Stop  ran Successfully - ");

			cts = true;
        }
    }
}

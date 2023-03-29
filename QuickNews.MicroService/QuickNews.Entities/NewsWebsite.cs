using QuickNews.Model;
using QuickNews.Utilities;

namespace QuickNews.Entities
{
    public interface INewsWebsite
    {
        Queue<M_NewsItem> NewsItems { get; set; }
        void Init(List<M_Rss> rssesList);
        Task CreateNewsItems(CancellationToken cancellationToken);
        //Task InsertNewsItem(CancellationToken cancellationToken);
        void Stop();
    }
    public class NewsWebsite
    {
        public INewsWebsite newsWebsite;

        public NewsWebsite(string provider, LogManager log)
        {
            switch (provider)
            {
                case "Ynet":
                    newsWebsite = new E_Ynet(log);
                    break;

                case "Walla":
                    newsWebsite = new E_Walla(log);
                    break;

                case "Maariv":
                    newsWebsite = new E_Maariv(log);
                    break;

                case "Globes":
                    newsWebsite = new E_Globes(log);
                    break;

                default:
                    newsWebsite = null;
                    break;
            }
        }
    }
}
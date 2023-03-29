using QuickNews.Model;
using QuickNews.Utilities;

namespace QuickNews.Entities
{
    public class S_CategoryService : BaseEntity
    {
        public S_CategoryService(LogManager log) : base(log) { }

        public void ClearList()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_CategoryService \ ClearList  ran Successfully - ");

				MainManager.Instance.categoriesList.Clear();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
            }
        }

        public List<M_Category> GetAllCategories()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_CategoryService \ GetAllCategories ran Successfully - ");

				MainManager.Instance.categoriesList = MainManager.Instance.db.Categories.ToList();
                return MainManager.Instance.categoriesList;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public M_Category GetCategoryById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_CategoryService \ GetCategoryById ran Successfully - ");

				M_Category Category = MainManager.Instance.db.Categories.Find(id);
                return Category;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void AddNewCategory(string topic)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_CategoryService \ AddNewCategory ran Successfully - ");

				M_Category Category = new M_Category
                {
                    Topic = topic
                };
                MainManager.Instance.categoriesList.Add(Category);
                MainManager.Instance.db.Categories.Add(Category);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
            }
        }

        public void UpdateCategoryById(int id, string topic)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_CategoryService \ UpdateCategoryById ran Successfully - ");

				M_Category Category = MainManager.Instance.db.Categories.Find(id);
                if (Category != null)
                {
                    Category.Topic = topic;
                    MainManager.Instance.categoriesList[id] = Category;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
            }
        }

        public void DeleteCategoryById(int id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_CategoryService \ DeleteCategoryById ran Successfully - ");

				M_Category Category = MainManager.Instance.db.Categories.Find(id);
                if (Category != null)
                {
                    MainManager.Instance.categoriesList.Remove(Category);
                    MainManager.Instance.db.Categories.Remove(Category);
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
            }
        }
    }
}

using QuickNews.Model;
using QuickNews.Utilities;

namespace QuickNews.Entities
{
    public class S_UserService : BaseEntity
    {
        public S_UserService(LogManager log) : base(log)
        {
        }

        public void ClearList()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_UserService \ ClearList  ran Successfully - ");

				MainManager.Instance.usersList.Clear();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public List<M_User> GetAllUsers()
        {
            try
            {
				Log.LogEvent(@"Entities \ S_UserService \ GetAllUsers  ran Successfully - ");

				MainManager.Instance.usersList = MainManager.Instance.db.Users.ToList();
                return MainManager.Instance.usersList;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);
				throw;
            }
        }

        public M_User GetUserById(string id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_UserService \ GetUserById  ran Successfully - ");

				M_User user = MainManager.Instance.db.Users.Find(id);
                return user;
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void AddNewUser(string userId, string name, string email, string phone)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_UserService \ AddNewUser  ran Successfully - ");

				M_User user = new M_User
                {
                    UserId = userId,
                    Name = name,
                    Email = email,
                    PhoneNumber = phone,
                    Interests = new List<M_Category> { MainManager.Instance.categoriesList.FirstOrDefault() } //each new user will get "BreakingNews" news even if he's not choosing- by deafult
                };
                MainManager.Instance.usersList.Add(user);
                MainManager.Instance.db.Users.Add(user);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void UpdateUserById(string id, string name, string phone)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_UserService \ UpdateUserById  ran Successfully - ");

				M_User user = MainManager.Instance.db.Users.Find(id);
                if (user != null)
                {
                    user.Name = name;
                    user.PhoneNumber = phone;
                    MainManager.Instance.usersList[MainManager.Instance.usersList.FindIndex(u => u.UserId == id)] = user;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void UpdateUserInterestsById(string id, string[] interests)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_UserService \ UpdateUserInterestsById  ran Successfully - ");

				M_User user = MainManager.Instance.db.Users.Find(id);
                if (user != null)
                {
                    List<M_Category> categories = MainManager.Instance.categoriesList.Where(c => interests.Contains(c.Topic)).ToList();
                    user.Interests = categories;
                    MainManager.Instance.usersList[MainManager.Instance.usersList.FindIndex(u => u.UserId == id)] = user;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
				Log.LogException($@"An Exception occurred while initializing the {ex.StackTrace} : {ex.Message}", ex);

				throw;
            }
        }

        public void DeleteUserById(string id)
        {
            try
            {
				Log.LogEvent(@"Entities \ S_UserService \ DeleteUserById  ran Successfully - ");

				M_User user = MainManager.Instance.db.Users.Find(id);
                if (user != null)
                {
                    MainManager.Instance.usersList.Remove(user);
                    MainManager.Instance.db.Users.Remove(user);
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


using CoWorkingApp.Data.Tools;
using CoWorkingApp.Models;

namespace CoWorkingApp.Data
{
    public class UserData
    {
        private JsonManager<User> jsonManager;
        public UserData()
        {
            jsonManager = new JsonManager<User>();
        }

        public (bool, bool) Login(string Email, string Password)
        {
            var userCollection = jsonManager.GetCollection();

            var passwordEncript = EncryptData.EncryptText(Password);
            var userFound = userCollection.FirstOrDefault(p => p.Email == Email && p.Password == passwordEncript);
            
            // User not found
            if (userFound == null) return (false, false);
            
            if (userFound.IsAdmin)
            {
                return (true, true);
            }
            else 
            {
                return (true, false);
            }
        }

        public bool CreateUser(User user)
        {
            user.Password = EncryptData.EncryptText(user.Password);

            try
            {
                var userCollection = jsonManager.GetCollection();
                userCollection.Add(user);
                jsonManager.SaveCollection(userCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditUser(User user)
        {
            try
            {
                user.Password = EncryptData.EncryptText(user.Password);
                var userCollection = jsonManager.GetCollection();

                var indexUser = userCollection.FindIndex(p => p.Email == user.Email);
                if (indexUser < 0)
                {
                    Console.WriteLine("The user does not exist");
                    return false;
                }

                if (indexUser >= userCollection.Count)
                {
                    Console.WriteLine("The index is out of bounds");
                    return false;
                }

                userCollection[indexUser] = user;
                jsonManager.SaveCollection(userCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public User? FindUser(string email)
        {
            var userCollection = jsonManager.GetCollection();
            var user = userCollection.FirstOrDefault(p => p.Email == email);
            if (user == null) return null;
            return user;
        }

        public bool DeleteUser(Guid userId)
        {
            try
            {
                var userCollection = jsonManager.GetCollection();
                var userToRemove = userCollection.Find(p => p.UserId == userId);

                if (userToRemove != null)
                {
                    userCollection.Remove(userToRemove);
                    jsonManager.SaveCollection(userCollection);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
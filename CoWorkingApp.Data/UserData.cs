
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

    }
}
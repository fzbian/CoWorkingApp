
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

        public bool CreateAdmin()
        {
            var userCollection = jsonManager.GetCollection();

            if(!userCollection.Any(p => p.Name == "ADMIN" && p.LastName == "ADMIN" && p.Email == "ADMIN"))
            {
                try
                {
                    var adminUser = new User()
                    {
                        Name = "ADMIN",
                        LastName = "ADMIN",
                        Email = "ADMIN",
                        UserId = Guid.NewGuid(),
                        Password = EncryptData.EncryptText("4DM1N@"),
                        IsAdmin = true
                    };
                    userCollection.Add(adminUser);
                    jsonManager.SaveCollection(userCollection);
                }
                catch
                {
                    return false;
                }      
                return true;
            }
            return true;
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

    }
}

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
                        Password = EncryptData.EncryptText("4DM1N@")
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
    }
}
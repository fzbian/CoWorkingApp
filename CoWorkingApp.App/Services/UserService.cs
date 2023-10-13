using CoWorkingApp.App.Enumerations;
using CoWorkingApp.Data;
using CoWorkingApp.Models;

namespace CoWorkingApp.App.Services
{
    public class UserService
    {
        private UserData userData { get; set; }
        public UserService(UserData userData)
        {
            this.userData = userData;
        }

        public void ExecAction(AdminUser menuAdminUserSelected)
        {
            switch (menuAdminUserSelected)
                    {
                        case AdminUser.Add:                  
                            var newUser = new User()
                            {
                                Name = HelperStrings.ReadInput("Type the name: "),
                                LastName = HelperStrings.ReadInput("Type the lastname: "),
                                Email = HelperStrings.ReadInput("Type the email: "),
                                Password = HelperStrings.ReadPassword("Type the password: "),
                                IsAdmin = HelperStrings.ReadBool("Is admin? (Y/n): ")
                            };

                            var createdUser = userData.CreateUser(newUser);

                            if (!createdUser)
                            {
                                Console.WriteLine("It cant be possible create this user.");
                            }
                            else
                            {
                                Console.WriteLine("User created succsesful!");
                            }
                            break;
                        case AdminUser.Edit:
                            Console.WriteLine("Option: edit");
                            break;
                        case AdminUser.Delete:
                            Console.WriteLine("Option: delete");
                            break;
                        case AdminUser.ChangePassword:
                            Console.WriteLine("Option: change password");
                            break;
            }
        }
    }
}
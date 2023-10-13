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
                            // TODO: Refactor this shit
                            var userEmail = HelperStrings.ReadInput("Type the email: ");
                            var userFound = userData.FindUser(userEmail);

                            while (userFound == null)
                            {
                                Console.WriteLine("User cant be found! Try again");
                                userEmail = HelperStrings.ReadInput("Type the email: ");
                                userFound = userData.FindUser(userEmail);
                            }

                            userFound = new User()
                            {
                                Name = HelperStrings.ReadInput("Type the name: "),
                                LastName = HelperStrings.ReadInput("Type the lastname: "),
                                Email = HelperStrings.ReadInput("Type the email: "),
                                Password = HelperStrings.ReadPassword("Type the password: "),
                                IsAdmin = HelperStrings.ReadBool("Is admin? (Y/n): ")
                            };

                            var res = userData.EditUser(userFound);

                            if (!res)
                            {
                                Console.WriteLine("It cant be possible edit this user.");
                            }
                            else
                            {
                                Console.WriteLine("User edited succsesful!");
                            }
                            break;
                        case AdminUser.Delete:
                            Console.WriteLine("Option: delete");
                            break;
                        case AdminUser.ChangePassword:
                            // TODO: Refactor this shit
                            var userEmailChangePassword = HelperStrings.ReadInput("Type the email: ");
                            var userFoundChangePassword = userData.FindUser(userEmailChangePassword);

                            while (userFoundChangePassword == null)
                            {
                                Console.WriteLine("User cant be found! Try again");
                                userEmailChangePassword = HelperStrings.ReadInput("Type the email: ");
                                userFoundChangePassword = userData.FindUser(userEmailChangePassword);
                            }

                            userFoundChangePassword.Password = HelperStrings.ReadPassword("Type the password: ");

                            var resChangePassword = userData.EditUser(userFoundChangePassword);

                            if (!resChangePassword)
                            {
                                Console.WriteLine("It can't be possible edit this user.");
                            }
                            else
                            {
                                Console.WriteLine("Password changed successfully!");
                            }

                            break;
            }
        }
    }
}
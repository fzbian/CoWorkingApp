using System.Collections.Concurrent;
using CoWorkingApp.App.Enumerations;
using CoWorkingApp.App.Services;
using CoWorkingApp.Data;
using CoWorkingApp.Models;

namespace CoWorkingApp.App
{
    class Program
    {
        static UserData UserDataService { get; set;} = new UserData();
        static UserService UserLogicService { get; set; } = new UserService(UserDataService);
        static void Main()
        {
            var appManager = new AppManager
            {
                AppName = "CoWorkingApp"
            };

            string? rolSelected = "";
            while (rolSelected != "1" && rolSelected != "2" || string.IsNullOrEmpty(rolSelected))
            {
                Console.WriteLine($"{appManager.AppName} - Menu");
                Console.WriteLine("1. Admin\n2. User");
                Console.Write("Select: ");
                rolSelected = Console.ReadLine();
            }

            if (Enum.Parse<UserRole>(rolSelected) == UserRole.Admin)
            {
                bool loginResult = false;
                
                while (!loginResult)
                {
                    Console.WriteLine("Login");
                    var emailLogin = HelperStrings.ReadInput("Email: ");
                    var passLogin = HelperStrings.ReadPassword("Password: ");

                    var (isLoggedIn, isAdmin) = UserDataService.Login(emailLogin, passLogin);
                    if(isLoggedIn)
                    {
                        if (isAdmin)
                        {
                            Console.WriteLine("Login succsesful.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Your user is not admin.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Email or password incorrect, try again!");
                    }
                    
                }

                string? menuAdminSelected = "";
                while (menuAdminSelected != "1" && menuAdminSelected != "2" || string.IsNullOrEmpty(menuAdminSelected))
                {
                    Console.WriteLine("1. Administration desks\n2. Administration users");
                    Console.Write("Select: ");
                    menuAdminSelected = Console.ReadLine();
                }   

                if (Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdminDesks)
                {
                    string? menuDesksSelected = "";
                    while (menuDesksSelected != "1" && menuDesksSelected != "2" && menuDesksSelected != "3" && menuDesksSelected != "4" || string.IsNullOrEmpty(menuDesksSelected))
                    {
                        Console.WriteLine("Administration desks");
                        Console.WriteLine("1. Create\n2. Edit\n3. Delete\n4. Block");
                        Console.Write("Select: ");
                        menuDesksSelected = Console.ReadLine();
                    }

                    AdminDesks menuAdminDesksSelected = Enum.Parse<AdminDesks>(menuDesksSelected);

                    switch (menuAdminDesksSelected)
                    {
                        case AdminDesks.Add:
                            Console.WriteLine("Option: create");
                            break;
                        case AdminDesks.Edit:
                            Console.WriteLine("Option: edit");
                            break;
                        case AdminDesks.Delete:
                            Console.WriteLine("Option: delete");
                            break;
                        case AdminDesks.Block:
                            Console.WriteLine("Option: block");
                            break;
                    }
                }
                else if (Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdminUsers)
                {
                    string? menuAdminUsersSelected = "";
                    while (menuAdminUsersSelected != "1" && menuAdminUsersSelected != "2" && menuAdminUsersSelected != "3" && menuAdminUsersSelected != "4" || string.IsNullOrEmpty(menuAdminUsersSelected))
                    {
                        Console.WriteLine("Administration users");
                        Console.WriteLine("1. Create\n2. Edit\n3. Delete\n4. Change password");
                        Console.Write("Select: ");
                        menuAdminUsersSelected = Console.ReadLine();
                    }

                    AdminUser menuAdminUserSelected = Enum.Parse<AdminUser>(menuAdminUsersSelected);

                    UserLogicService.ExecAction(menuAdminUserSelected);

                }
            }
            else if (Enum.Parse<UserRole>(rolSelected) == UserRole.User)
            {
                string? menuUserSelected = "";

                while (menuUserSelected != "1" && menuUserSelected != "2" && menuUserSelected != "3" && menuUserSelected != "4" || string.IsNullOrEmpty(menuUserSelected))
                {
                    Console.WriteLine("User options");
                    Console.WriteLine("1. Reserve desk\n2. Cancel reserve\n3. History reserve\n4. Change password");
                    Console.Write("Select: ");
                    menuUserSelected = Console.ReadLine();
                }
 
                MenuUser menuUsersSelected = Enum.Parse<MenuUser>(menuUserSelected);
                
                switch (menuUsersSelected)
                {
                    case MenuUser.ReserveDesk:
                        Console.WriteLine("Option: reserve desk");
                        break;
                    case MenuUser.CancelReserve:
                        Console.WriteLine("Option: cancel reserve");
                        break;
                    case MenuUser.HistoryReserve:
                        Console.WriteLine("Option: history reserves");
                        break;
                    case MenuUser.ChangePassword:
                        Console.WriteLine("Option: change password");
                        break;
                }
            }
        }
    }
}
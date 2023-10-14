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
        static DeskData DeskDataService { get; set; } = new DeskData();
        static UserService UserLogicService { get; set; } = new UserService(UserDataService);
        static DeskService DeskLogicService { get; set; } = new DeskService(DeskDataService);
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
                rolSelected = HelperStrings.ReadInput("1. Admin\n2. User\nSelect: ");
            }

            if (Enum.Parse<UserRole>(rolSelected) == UserRole.Admin)
            {
                UserLogicService.LoginUser(true);

                string? menuAdminSelected = "";
                while (menuAdminSelected != "1" && menuAdminSelected != "2" || string.IsNullOrEmpty(menuAdminSelected))
                {
                    menuAdminSelected = HelperStrings.ReadInput("1. Administration desks\n2. Administration users\nSelect: ");
                }   

                if (Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdminDesks)
                {
                    string? menuDesksSelected = "";
                    while (menuDesksSelected != "1" && menuDesksSelected != "2" && menuDesksSelected != "3" && menuDesksSelected != "4" || string.IsNullOrEmpty(menuDesksSelected))
                    {
                        Console.WriteLine("Administration desks");
                        menuDesksSelected = HelperStrings.ReadInput("1. Create\n2. Edit\n3. Delete\n4. Block\nSelect: ");
                    }

                    AdminDesks menuAdminDesksSelected = Enum.Parse<AdminDesks>(menuDesksSelected);

                    DeskLogicService.ExecAction(menuAdminDesksSelected);
                }
                else if (Enum.Parse<MenuAdmin>(menuAdminSelected) == MenuAdmin.AdminUsers)
                {
                    string? menuAdminUsersSelected = "";
                    while (menuAdminUsersSelected != "1" && menuAdminUsersSelected != "2" && menuAdminUsersSelected != "3" && menuAdminUsersSelected != "4" || string.IsNullOrEmpty(menuAdminUsersSelected))
                    {
                        Console.WriteLine("Administration users");
                        menuAdminUsersSelected = HelperStrings.ReadInput("1. Create\n2. Edit\n3. Delete\n4. Change password\nSelect: ");
                    }

                    AdminUser menuAdminUserSelected = Enum.Parse<AdminUser>(menuAdminUsersSelected);

                    UserLogicService.ExecAction(menuAdminUserSelected);

                }
            }
            else if (Enum.Parse<UserRole>(rolSelected) == UserRole.User)
            {
                UserLogicService.LoginUser(false);

                string? menuUserSelected = "";

                while (menuUserSelected != "1" && menuUserSelected != "2" && menuUserSelected != "3" && menuUserSelected != "4" || string.IsNullOrEmpty(menuUserSelected))
                {
                    Console.WriteLine("User options");
                    menuUserSelected = HelperStrings.ReadInput("1. Reserve desk\n2. Cancel reserve\n3. History reserve\n4. Change password\nSelect: ");
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
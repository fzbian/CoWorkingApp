namespace CoWorkingApp.App
{
    class Program
    {
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

            if (rolSelected == "1")
            {
                string? menuAdminSelected = "";
                while (menuAdminSelected != "1" && menuAdminSelected != "2" || string.IsNullOrEmpty(menuAdminSelected))
                {
                    Console.WriteLine("1. Administration desks\n2. Administration users");
                    Console.Write("Select: ");
                    menuAdminSelected = Console.ReadLine();
                }   

                if (menuAdminSelected == "1")
                {
                    string? menuDesksSelected = "";
                    while (menuDesksSelected != "1" && menuDesksSelected != "2" && menuDesksSelected != "3" && menuDesksSelected != "4" || string.IsNullOrEmpty(menuDesksSelected))
                    {
                        Console.WriteLine("Administration desks");
                        Console.WriteLine("1. Create\n2. Edit\n3. Delete\n4. Block");
                        Console.Write("Select: ");
                        menuDesksSelected = Console.ReadLine();
                    }

                    switch (menuDesksSelected)
                    {
                        case "1":
                            Console.WriteLine("Option: create");
                            break;
                        case "2":
                            Console.WriteLine("Option: edit");
                            break;
                        case "3":
                            Console.WriteLine("Option: delete");
                            break;
                        case "4":
                            Console.WriteLine("Option: block");
                            break;
                    }
                }
                else if (menuAdminSelected == "2")
                {
                    string? menuAdminUsersSelected = "";
                    while (menuAdminUsersSelected != "1" && menuAdminUsersSelected != "2" && menuAdminUsersSelected != "3" && menuAdminUsersSelected != "4" || string.IsNullOrEmpty(menuAdminUsersSelected))
                    {
                        Console.WriteLine("Administration users");
                        Console.WriteLine("1. Create\n2. Edit\n3. Delete\n4. Change password");
                        Console.Write("Select: ");
                        menuAdminUsersSelected = Console.ReadLine();
                    }

                    switch (menuAdminUsersSelected)
                    {
                        case "1":
                            Console.WriteLine("Option: create");
                            break;
                        case "2":
                            Console.WriteLine("Option: edit");
                            break;
                        case "3":
                            Console.WriteLine("Option: delete");
                            break;
                        case "4":
                            Console.WriteLine("Option: change password");
                            break;
                    }
                }
            }
            else if (rolSelected == "2")
            {
                string? menuUserSelected = "";

                while (menuUserSelected != "1" && menuUserSelected != "2" && menuUserSelected != "3" && menuUserSelected != "4" || string.IsNullOrEmpty(menuUserSelected))
                {
                    Console.WriteLine("User options");
                    Console.WriteLine("1. Reserve desk\n2. Cancel reserve\n3. History reserve\n4. Change password");
                    Console.Write("Select: ");
                    menuUserSelected = Console.ReadLine();
                }
                
                switch (menuUserSelected)
                {
                    case "1":
                        Console.WriteLine("Option: reserve desk");
                        break;
                    case "2":
                        Console.WriteLine("Option: cancel reserve");
                        break;
                    case "3":
                        Console.WriteLine("Option: history reserves");
                        break;
                    case "4":
                        Console.WriteLine("Option: change password");
                        break;
                }
            }
        }
    }
}
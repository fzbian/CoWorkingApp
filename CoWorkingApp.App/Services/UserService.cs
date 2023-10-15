using CoWorkingApp.App.Enumerations;
using CoWorkingApp.Data;
using CoWorkingApp.Models;

namespace CoWorkingApp.App.Services
{
    public class UserService
    {
        private UserData userData { get; set; }
        private DeskData deskData { get; set; }
        private ReservationData reservationData { get; set; }
        public UserService(UserData userData, DeskData deskData)
        {
            this.userData = userData;
            this.deskData = deskData;
            this.reservationData = new ReservationData();
        }

        public void ExecActionAdmin(AdminUser menuAdminUserSelected)
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
                            var userEmailDelete = HelperStrings.ReadInput("Type the email: ");
                            var userFoundDelete = userData.FindUser(userEmailDelete);

                            while (userFoundDelete == null)
                            {
                                Console.WriteLine("User cant be found! Try again");
                                userEmailDelete = HelperStrings.ReadInput("Type the email: ");
                                userFoundDelete = userData.FindUser(userEmailDelete);
                            }

                            var deleteOption = HelperStrings.ReadBool($"Are you sure you want to delete the user '{userFoundDelete.Name} {userFoundDelete.LastName}'? (Y/n): ");                          
                            if (deleteOption)
                            {
                                userData.DeleteUser(userFoundDelete.UserId);
                                Console.WriteLine("User removed");
                            }
                            else
                            {
                                Console.WriteLine("User cant be removed!");
                            }
                            break;
                        case AdminUser.ChangePassword:
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

        public void ExecActionUser(User ActiveUser, MenuUser menuUserSelected)
        {
            switch (menuUserSelected)
            {
                case MenuUser.ReserveDesk:
                    var deskList = deskData.GetDesks();
                    foreach(var item in deskList)
                    {
                        Console.WriteLine($"{item.Number} - {item.DeskStatus}");
                    }

                    var newReservation = new Reservation();

                    var deskNumber = HelperStrings.ReadInput("Type the number desk: ");
                    var deskFound = deskData.FindDesk(deskNumber);

                    while(deskFound == null)
                    {
                        deskNumber = HelperStrings.ReadInput("Type the number desk: ");
                        deskFound = deskData.FindDesk(deskNumber);
                    }

                    newReservation.DeskId = deskFound.DeskId;

                    var dateSelected = new DateTime();

                    while(dateSelected.Year == 0001)
                    {
                        DateTime.TryParseExact(HelperStrings.ReadInput("Type the date of the reservation (dd-mm-yyyy): "), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dateSelected);
                    }

                    newReservation.ReservationDate = dateSelected;
                    newReservation.UserId = ActiveUser.UserId;
                    
                    if(reservationData.CreateReservation(newReservation))
                    {
                        Console.WriteLine("Reservation created succsesfully.");
                    }
                    else
                    {
                        Console.WriteLine("The reservation cant be created.");
                    }
                    break;
                case MenuUser.CancelReserve:
                    Console.WriteLine("Your reservations: ");
                    var userReservations = reservationData.GetReservationsByUser(ActiveUser.UserId).ToList();
                    
                    int indexReservation = 1;
                    var deskUserList = deskData.GetDesks();

                    foreach(var item in userReservations)
                    {
                        Console.WriteLine($"{indexReservation}: {deskUserList.FirstOrDefault(p => p.DeskId == item.DeskId)} - {item.ReservationDate.ToString("dd-MM-yyy")}");
                        indexReservation++;
                    }

                    var indexReservationSelected = int.Parse(HelperStrings.ReadInput("Type the number of reservation: "));

                    while(indexReservationSelected >= 1 && indexReservationSelected <= indexReservation)
                    {
                        indexReservationSelected = int.Parse(HelperStrings.ReadInput("Type the number of reservation: "));
                    }

                    var reservatioToDelete = userReservations[indexReservationSelected];

                    if(reservationData.CancelReservation(reservatioToDelete.ReservationId))
                    {
                        Console.WriteLine("The reservation was cancelled.");
                    }         
                    else
                    {
                        Console.WriteLine("Reservation cant be deleted.");
                    }         
                    break;
                case MenuUser.HistoryReserve:
                    var userReservationsHistory = reservationData.GetReservationsByUser(ActiveUser.UserId).ToList();
                    foreach(var item in userReservationsHistory)
                    {
                        Console.WriteLine($"{userReservationsHistory.FirstOrDefault(p => p.DeskId == item.DeskId)} - {item.ReservationDate.ToString("dd-MM-yyy")}");
                    }
                    break;
                case MenuUser.ChangePassword:
                    Console.WriteLine("Option: change password");
                    break;
            }

        }

        public User? LoginUser(bool isUserAdmin)
        {
            Console.WriteLine("Login");
            var emailLogin = HelperStrings.ReadInput("Email: ");
            var passLogin = HelperStrings.ReadPassword("Password: ");

            var (user, isAdmin) = userData.Login(emailLogin, passLogin);
        
            while(user == null)
            {
                Console.WriteLine("Email or password incorrect, try again!");
                Console.WriteLine("Login");
                emailLogin = HelperStrings.ReadInput("Email: ");
                passLogin = HelperStrings.ReadPassword("Password: ");
                (user, isAdmin) = userData.Login(emailLogin, passLogin);
            }

            while (isUserAdmin && !isAdmin || !isUserAdmin && isAdmin)
            {
                Console.WriteLine("User is not in the correct panel, try again!");
                Console.WriteLine("Login");
                emailLogin = HelperStrings.ReadInput("Email: ");
                passLogin = HelperStrings.ReadPassword("Password: ");
                (user, isAdmin) = userData.Login(emailLogin, passLogin);
            }
            
            return user;
        }
    }
}
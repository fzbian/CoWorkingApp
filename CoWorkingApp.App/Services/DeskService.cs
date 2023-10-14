using CoWorkingApp.App.Enumerations;
using CoWorkingApp.Data;
using CoWorkingApp.Models;
using CoWorkingApp.Models.Enumerations;

namespace CoWorkingApp.App.Services
{
    public class DeskService
    {
        private DeskData deskData { get; set; }
        public DeskService(DeskData deskData)
        {
            this.deskData = deskData;
        }
        public void ExecAction(AdminDesks adminDeskAction)
        {
            switch (adminDeskAction)
            {
                case AdminDesks.Add:
                    var newDesk = new Desk()
                    {
                        Number = HelperStrings.ReadInput("Type the number desk: "),
                        Description = HelperStrings.ReadInput("Type the description: "),
                        DeskStatus = DeskStatus.Active      
                    };

                    var createdDesk = deskData.CreateDesk(newDesk);
                    
                    if(!createdDesk)
                    {
                        Console.WriteLine("It cant be possible create this desk");
                    }
                    else
                    {
                        Console.WriteLine($"Desk '{newDesk.Number}' created.");
                    }
                    break;
                case AdminDesks.Edit:
                    var deskNumber = HelperStrings.ReadInput("Type the number desk: ");
                    var deskFound = deskData.FindDesk(deskNumber);

                    while(deskFound == null)
                    {
                        deskNumber = HelperStrings.ReadInput("Type the number desk: ");
                        deskFound = deskData.FindDesk(deskNumber);
                    }

                    Console.WriteLine($"Desk founded\nNumber: {deskFound.Number}\nDescription: {deskFound.Description}\nStatus: {deskFound.DeskStatus}");

                    var editDesk = new Desk()
                    {
                        Number = HelperStrings.ReadInput("Type the new number desk: "),
                        Description = HelperStrings.ReadInput("Type the new description: "),       
                        DeskStatus = HelperStrings.ReadDeskStatus("Type the new status (1. Active, 2. Inactive, 3. Blocked): ")
                    };

                    var editedDesk = deskData.EditDesk(editDesk);
                    
                    if(!editedDesk)
                    {
                        Console.WriteLine("It cant be possible edit this desk");
                    }
                    else
                    {
                        Console.WriteLine($"Desk '{editDesk.Number}' edited.");
                    }
                    break;
                case AdminDesks.Delete:
                    var deskNumberDelete = HelperStrings.ReadInput("Type the number desk: ");
                    var deskFoundDelete = deskData.FindDesk(deskNumberDelete);

                    while(deskFoundDelete == null)
                    {
                        deskNumberDelete = HelperStrings.ReadInput("Type the number desk: ");
                        deskFoundDelete = deskData.FindDesk(deskNumberDelete);
                    }

                    var deletedDesk = deskData.DeleteDesk(deskFoundDelete.DeskId);

                    if(!deletedDesk)
                    {
                        Console.WriteLine("It cant be possible delete this desk");
                    }
                    else
                    {
                        Console.WriteLine($"Desk '{deskFoundDelete.Number}' deleted.");
                    }
                    break;
            }
        }
    }
}

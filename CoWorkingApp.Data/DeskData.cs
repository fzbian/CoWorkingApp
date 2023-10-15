using CoWorkingApp.Models;

namespace CoWorkingApp.Data
{
    public class DeskData
    {
        private JsonManager<Desk> jsonManager;
        public DeskData()
        {
            jsonManager = new JsonManager<Desk>();
        }
        public Desk? FindDesk(string numberDesk)
        {
            try
            {
                var deskCollection = jsonManager.GetCollection();
                var desk = deskCollection.FirstOrDefault(p => p.Number == numberDesk);
                if(desk == null) return null;
                return desk;
            }
            catch
            {
                return null;
            }
        }
        public bool CreateDesk(Desk desk)
        {
            try
            {
                var deskCollection = jsonManager.GetCollection();
                deskCollection.Add(desk);
                jsonManager.SaveCollection(deskCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditDesk(Desk desk)
        {
            try
            {
                var deskCollection = jsonManager.GetCollection();

                // TODO: This should be searched by the DeskId, not by the number
                var indexDesk = deskCollection.FindIndex(p => p.Number == desk.Number);
                if (indexDesk < 0)
                {
                    Console.WriteLine("The desk does not exist");
                    return false;
                }

                if (indexDesk >= deskCollection.Count)
                {
                    Console.WriteLine("The index is out of bounds");
                    return false;
                }

                deskCollection[indexDesk] = desk;
                jsonManager.SaveCollection(deskCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDesk(Guid deskId)
        {
            try
            {
                var deskCollection = jsonManager.GetCollection();
                var indexDesk = deskCollection.FindIndex(p => p.DeskId == deskId);
                deskCollection.RemoveAt(indexDesk);
                jsonManager.SaveCollection(deskCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Desk> GetDesks()
        {
            return jsonManager.GetCollection();
        }
    }
}
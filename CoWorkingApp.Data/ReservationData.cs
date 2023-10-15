using CoWorkingApp.Models;

namespace CoWorkingApp.Data
{
    public class ReservationData
    {
        private JsonManager<Reservation> jsonManager;

        public ReservationData()
        {
            jsonManager = new JsonManager<Reservation>();
        }
        public bool CreateReservation(Reservation newReservation)
        {
            try
            {
                var reservationCollection = jsonManager.GetCollection();
                reservationCollection.Add(newReservation);
                jsonManager.SaveCollection(reservationCollection);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
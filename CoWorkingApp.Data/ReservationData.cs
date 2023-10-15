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

        public bool CancelReservation(Guid reservationId)
        {
            try
            {
                var reservationCollection = jsonManager.GetCollection();
                var reservationToRemove = reservationCollection.Find(p => p.ReservationId == reservationId);

                if (reservationToRemove != null)
                {
                    reservationCollection.Remove(reservationToRemove);
                    jsonManager.SaveCollection(reservationCollection);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Reservation> GetReservationsByUser(Guid userId)
        {
            var reservationCollection = jsonManager.GetCollection();
            return reservationCollection.Where(p => p.UserId == userId && p.ReservationDate > DateTime.Now);
        }
    }
}
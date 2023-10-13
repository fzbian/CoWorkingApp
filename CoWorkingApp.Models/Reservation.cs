using System;

namespace CoWorkingApp.Models
{
    public class Reservetion
    {
        public Guid ReservetionId { get; set; }
        public DateTime ReservetionDate { get; set; }
        public Guid DeskId { get; set; }
        public Guid UserId { get; set; }
    }
}
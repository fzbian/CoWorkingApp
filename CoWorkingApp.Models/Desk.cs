using System;

namespace CoWorkingApp.Models
{
    public class Desk
    {
        public Guid DeskId { get; set; }
        public required string Number { get; set; }
        public required string Description { get; set; }
    }
}
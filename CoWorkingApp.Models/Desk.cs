using System;
using CoWorkingApp.Models.Enumerations;

namespace CoWorkingApp.Models
{
    public class Desk
    {
        public Guid DeskId { get; set; } = Guid.NewGuid();
        public required string Number { get; set; }
        public required string Description { get; set; }
        public DeskStatus DeskStatus { get; set; } = DeskStatus.Active;
    }
}
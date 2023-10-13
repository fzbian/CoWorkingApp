using System;

namespace CoWorkingApp.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool IsAdmin { get; set; }
    }
}
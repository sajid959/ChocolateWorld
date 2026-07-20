using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class User
    {
        private User() { }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Email { get; private set; }
        public string Phone {  get; private set; }
        public string Role { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;

        public User(Guid id, string name, string? email, string phone, string role, DateTimeOffset cretedAt) {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Role = role;
            CreatedAt = cretedAt;
        }

        public void UpdateProfile(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}

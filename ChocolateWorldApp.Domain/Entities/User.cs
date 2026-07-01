using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; }
        public string Name { get; }
        public string? Email { get; }
        public string Phone {  get; }
        public string Role { get; }
        public DateTimeOffset? CreatedAt { get; } = DateTimeOffset.Now;

        public User(Guid id, string name, string? email, string phone, string role, DateTimeOffset cretedAt) {
            id = Id;
            Name = name;
            Email = email;
            Phone = phone;
            Role = role;
            CreatedAt = cretedAt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string RecipientName { get; }
        public string RecipientPhone { get; }
        public string Line1 { get; }
        public string? Line2 { get; }
        public string City { get; }

        public string State { get; }
        public string PinCode { get; }

        public Address(
            Guid id,
            Guid userId,
            string recipientName,
            string recipientPhone,
            string lin1,
            string line2,
            string city,
            string state,
            string pinCode) 
        {
            Id = id;    
            UserId = userId;
            RecipientName = recipientName;
            RecipientPhone = recipientPhone;
            Line1 = lin1;
            Line2 = line2;
            City = city;
            State = state;
            PinCode = pinCode;
        }
    }
}

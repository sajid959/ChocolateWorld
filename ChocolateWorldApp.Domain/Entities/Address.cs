using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class Address
    {
        private Address() { }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string RecipientName { get; private set; }
        public string RecipientPhone { get; private set; }
        public string Line1 { get; private set; }
        public string? Line2 { get; private set; }
        public string City { get; private set; }

        public string State { get; private set; }
        public string PinCode { get; private set; }

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

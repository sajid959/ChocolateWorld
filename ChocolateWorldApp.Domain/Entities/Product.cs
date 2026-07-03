using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class Product
    {
        private Product() { }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string OccasionTags { get; private set; }
        public string AttributesJson { get; private set; }
        public bool IsActive { get; private set; }

        public Product(
            Guid id,
            string name,
            string description,
            string occasionTags,
            string attributesJson)
        { 
            Id = id;
            Name = name;
            Description = description;
            OccasionTags = occasionTags;
            AttributesJson = attributesJson;
            IsActive = true;

        }
    }
}

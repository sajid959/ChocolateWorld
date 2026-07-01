using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string OccasionTags { get; }
        public string AttributesJson { get; }
        public bool IsActive { get; }

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

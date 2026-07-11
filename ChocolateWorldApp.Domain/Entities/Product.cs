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
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }   //Navigation Property for Join
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string OccasionTags { get; private set; }
        public string AttributesJson { get; private set; }
        public bool IsActive { get; private set; }
        
        public ICollection<ProductVariant> ProductVariants { get; private set; } = new List<ProductVariant>();

        public Product(
            Guid id,
            Guid categoryId,
            Category category,
            string name,
            string  slug,
            string description,
            string occasionTags,
            string attributesJson)
        { 
            Id = id;
            CategoryId = categoryId;
            Category = category;
            Name = name;
            Slug = slug;
            Description = description;
            OccasionTags = occasionTags;
            AttributesJson = attributesJson;
            IsActive = true;

        }
    }
}

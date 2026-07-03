using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class Category
    {
        private Category() { }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public Guid? ParentcategoryId { get; private set; }
        public int DisplayOrder {  get; private set; }
        public bool IsActive { get; private set; }

        public Category(
            Guid id,
            string name,
            string slug,
            string description,
            Guid? parentCategoryId,
            int displayOrder,
            bool isActive)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Description = description;
            ParentcategoryId = parentCategoryId;
            DisplayOrder = displayOrder;
            IsActive = isActive;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class Category
    {
        public Guid Id { get;}
        public string Name { get;}
        public string Slug { get;}
        public string Description { get;}
        public Guid? ParentcategoryId { get;}
        public int DisplayOrder {  get;}
        public bool IsActive { get;}

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

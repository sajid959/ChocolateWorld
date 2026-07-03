using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class ProductVariant
    {
        private ProductVariant() { }
        public Guid Id { get; private set; }
        public decimal Price { get; private set; }
        public int BoxSize { get; private set; }
        public Guid ProductId { get; private set; }

        public ProductVariant(Guid id, decimal price, int boxSize, Guid productId)
        {
            Id= id;
            Price= price;
            BoxSize= boxSize;
            ProductId= productId;
        }
    }
}

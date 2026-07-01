using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class ProductVariant
    {
        public Guid Id { get; }
        public decimal Price { get;}
        public int BoxSize { get;}
        public Guid ProductId { get;}

        public ProductVariant(Guid id, decimal price, int boxSize, Guid productId)
        {
            Id= id;
            Price= price;
            BoxSize= boxSize;
            ProductId= productId;
        }
    }
}

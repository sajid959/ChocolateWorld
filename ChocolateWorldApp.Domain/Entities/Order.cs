using ChocolateWorldApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Entities
{
    public class Order
    {
        private Order() { }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid DeliverAddressId { get; private set; }
        public Guid ParentOrderId { get; private set; }
        public Guid B2BAccountId { get; private set; }
        public OrderStatus Status { get; private set; }
        public string PaymentMethod { get; private set; }
        public DateOnly DeliveryDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public Order(
            Guid id,
            Guid userId,
            Guid deliveryAddressId,
            Guid parentOrderId,
            Guid b2bAccountId,
            OrderStatus status,
            string paymentMethod,
            DateOnly deliverDate,
            decimal totalAmount,
            DateTimeOffset createdAt)
        {
            Id = id;
            UserId = userId;
            DeliverAddressId = deliveryAddressId;
            ParentOrderId = parentOrderId;
            B2BAccountId = b2bAccountId;
            Status = status;
            PaymentMethod = paymentMethod;
            DeliveryDate = deliverDate;
            TotalAmount = totalAmount;
            CreatedAt = createdAt;
        }

    }
}

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
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid DeliverAddressId { get; }
        public Guid ParentOrderId { get; }
        public Guid B2BAccountId { get; }
        public OrderStatus Status { get; }
        public string PaymentMethod { get; }
        public DateOnly DeliveryDate { get; }
        public decimal TotalAmount { get; }
        public DateTimeOffset CreatedAt { get; }

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

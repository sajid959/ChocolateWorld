using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateWorldApp.Domain.Enums
{
    public enum OrderStatus
    {
        Placed,
        PaymentConfirmed,
        CODConfirmed,
        Packed,
        AssignedToVendor,
        OutForDelivery,
        Delivered,
        Cancelled,
        Failed,
        Returned
    }
}

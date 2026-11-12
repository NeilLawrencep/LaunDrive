
using System;

namespace LaunDrive.Models
{
    public class LaundryOrder
    {
        // Order Information
        public int LaundryOrderId { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Customer Information
        public string CustomerName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        // Laundry Services
        public bool Wash { get; set; }
        public bool Iron { get; set; }
        public bool Fold { get; set; }

        // Laundry Information
        public int NumberOfPieces { get; set; }
        public string SpecialInstructions { get; set; } = string.Empty;

        // Pickup and Delivery Details
        public string PickupAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;

        // Schedule Selection
        public DateTime PickupSchedule { get; set; }
        public DateTime DeliverySchedule { get; set; }

        // Pricing
        public decimal TotalPrice { get; set; }

        // Laundry Status
        public string OrderStatus { get; set; } = "Pending";

        // Payment Information
        public string PaymentMethod { get; set; } = "Cash on Delivery";
        public string PaymentStatus { get; set; } = "Unpaid";
    }
}

using LaunDrive.Models;
using LaunDrive.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using LaunDrive.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaunDrive.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly LaunDriveDbContext _db;

        public CreateModel(LaunDriveDbContext db)
        {
            _db = db;
        }


        [BindProperty]
        public OrderInputModel Input { get; set; } = new();

        public class OrderInputModel
        {
            // Customer information
            [Required]
            public string CustomerName { get; set; } = string.Empty;

            [Required]
            public string ContactNumber { get; set; } = string.Empty;

            // Laundry services
            public bool Wash { get; set; }
            public bool Iron { get; set; }
            public bool Fold { get; set; }

            // Number of clothing pieces
            [Range(1, int.MaxValue)]
            public int NumberOfPieces { get; set; }

            // Pickup and delivery
            [Required]
            public string PickupAddress { get; set; } = string.Empty;

            [Required]
            public string DeliveryAddress { get; set; } = string.Empty;

            // Schedules
            [Required]
            public DateTime? PickupSchedule { get; set; }

            [Required]
            public DateTime? DeliverySchedule { get; set; }

            // Optional instructions
            public string? SpecialInstructions { get; set; }
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if(!Input.Wash && !Input.Iron && !Input.Fold)
            {
                ModelState.AddModelError("", "Please select at least one laundry service.");
                return Page();
            }
            if (Input.PickupSchedule!.Value <= DateTime.Now)
            {
                ModelState.AddModelError(
                    "",
                    "Pickup schedule must be in the future."
                );

                return Page();
            }

            if (Input.DeliverySchedule!.Value <= Input.PickupSchedule.Value)
            {
                ModelState.AddModelError(
                    "",
                    "Delivery must be scheduled after pickup."
                );

                return Page();
            }
            decimal total = LaundryPricingService.CalculateTotal(
                Input.Wash,
                Input.Iron,
                Input.Fold,
                Input.NumberOfPieces
            );

            LaundryOrder order = new LaundryOrder
            {
                TrackingNumber = "LD-" + Guid.NewGuid().ToString("N").ToUpperInvariant(),

                CustomerName = Input.CustomerName,
                ContactNumber = Input.ContactNumber,

                Wash = Input.Wash,
                Iron = Input.Iron,
                Fold = Input.Fold,

                NumberOfPieces = Input.NumberOfPieces,
                SpecialInstructions = Input.SpecialInstructions ?? string.Empty,

                PickupAddress = Input.PickupAddress,
                DeliveryAddress = Input.DeliveryAddress,

                PickupSchedule = Input.PickupSchedule.Value,
                DeliverySchedule = Input.DeliverySchedule.Value,

                TotalPrice = total,
                OrderStatus = "Pending",
                PaymentMethod = "Cash on Delivery",
                PaymentStatus = "Unpaid"
            };

            _db.LaundryOrders.Add(order);

            await _db.SaveChangesAsync();

            return RedirectToPage(
                "/Customer/Confirmation",
                new { id = order.LaundryOrderId }
            );
        
    }

    }
}

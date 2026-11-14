
using LaunDrive.Data;
using LaunDrive.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LaunDrive.Pages.Customer
{
    public class TrackModel : PageModel
    {
        private readonly LaunDriveDbContext _db;

        public TrackModel(LaunDriveDbContext db)
        {
            _db = db;
        }

        // Tracking number entered by the customer
        [BindProperty]
        [Required(ErrorMessage = "Please enter your tracking number.")]
        public string TrackingNumber { get; set; } = string.Empty;

        // Order found in the database
        public LaundryOrder? Order { get; private set; }

        // Runs when the customer opens the page
        public void OnGet()
        {

        }

        // Runs when the customer searches for an order
        public async Task<IActionResult> OnPostAsync()
        {
            // Step 1: Check the input
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Step 2: Clean the tracking number
            string trackingNumber =
                TrackingNumber.Trim().ToUpperInvariant();

            // Step 3: Search the database
            Order = await _db.LaundryOrders
                .AsNoTracking()
                .FirstOrDefaultAsync(order =>
                    order.TrackingNumber == trackingNumber);

            // Step 4: Check if the order exists
            if (Order == null)
            {
                ModelState.AddModelError(
                    nameof(TrackingNumber),
                    "No laundry order found with that tracking number."
                );

                return Page();
            }

            // Step 5: Display the found order
            return Page();
        }
    }
}


using LaunDrive.Data;
using LaunDrive.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaunDrive.Pages.Customer
{
    public class ConfirmationModel : PageModel
    {
        private readonly LaunDriveDbContext _db;

        public ConfirmationModel(LaunDriveDbContext db)
        {
            _db = db;
        }

        public LaundryOrder? Order { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _db.LaundryOrders
                .AsNoTracking()
                .FirstOrDefaultAsync(order =>
                    order.LaundryOrderId == id.Value);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

using BloodBankManagementSystem.Data;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagementSystem.Controllers
{
    public class BloodStockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodStockController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stocks = await _context.BloodStocks
                .OrderBy(s => s.BloodGroup)
                .ToListAsync();
            return View(stocks);
        }

        // Add or remove units: positive number adds, negative number removes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adjust(int id, int units)
        {
            var stock = await _context.BloodStocks.FindAsync(id);
            if (stock == null) return NotFound();

            if (units == 0)
            {
                TempData["Error"] = "Please enter a number other than 0.";
                return RedirectToAction(nameof(Index));
            }

            if (stock.Units + units < 0)
            {
                TempData["Error"] = $"Cannot remove {-units} units. Only {stock.Units} available.";
                return RedirectToAction(nameof(Index));
            }

            stock.Units += units;
            stock.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["Success"] = units > 0
                ? $"Added {units} unit(s) to {stock.BloodGroup}."
                : $"Removed {-units} unit(s) from {stock.BloodGroup}.";
            return RedirectToAction(nameof(Index));
        }
    }
}
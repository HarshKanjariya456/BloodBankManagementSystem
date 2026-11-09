using BloodBankManagementSystem.Data;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagementSystem.Controllers
{
    public class DonorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? city, BloodGroup? bloodGroup)
        {
            var donors = _context.Donors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
                donors = donors.Where(d => d.City.Contains(city));

            if (bloodGroup.HasValue)
                donors = donors.Where(d => d.BloodGroup == bloodGroup.Value);

            ViewData["City"] = city;
            ViewData["BloodGroups"] = new SelectList(Enum.GetValues<BloodGroup>(), bloodGroup);

            return View(await donors.OrderBy(d => d.FullName).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var donor = await _context.Donors.FirstOrDefaultAsync(d => d.Id == id);
            if (donor == null) return NotFound();
            return View(donor);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("FullName,Age,Gender,BloodGroup,Phone,Email,City,LastDonationDate")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null) return NotFound();
            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,FullName,Age,Gender,BloodGroup,Phone,Email,City,LastDonationDate")] Donor donor)
        {
            if (id != donor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Donors.Any(d => d.Id == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var donor = await _context.Donors.FirstOrDefaultAsync(d => d.Id == id);
            if (donor == null) return NotFound();
            return View(donor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor != null)
            {
                _context.Donors.Remove(donor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecordDonation(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null) return NotFound();

            if (!donor.IsEligible)
            {
                TempData["Error"] = $"{donor.FullName} is not eligible until {donor.NextEligibleDate:dd MMM yyyy}.";
                return RedirectToAction(nameof(Index));
            }

            var stock = await _context.BloodStocks
                .FirstOrDefaultAsync(s => s.BloodGroup == donor.BloodGroup);

            if (stock == null)
            {
                stock = new BloodStock { BloodGroup = donor.BloodGroup, Units = 0 };
                _context.BloodStocks.Add(stock);
            }

            stock.Units += 1;
            stock.LastUpdated = DateTime.Now;
            donor.LastDonationDate = DateTime.Today;

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Donation recorded for {donor.FullName}. 1 unit added to {donor.BloodGroup}.";
            return RedirectToAction(nameof(Index));
        }
    }
}
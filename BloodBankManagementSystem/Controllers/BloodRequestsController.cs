using BloodBankManagementSystem.Data;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagementSystem.Controllers
{
    public class BloodRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(RequestStatus? status)
        {
            var requests = _context.BloodRequests.AsQueryable();

            if (status.HasValue)
                requests = requests.Where(r => r.Status == status.Value);

            ViewData["Statuses"] = new SelectList(Enum.GetValues<RequestStatus>(), status);

            return View(await requests.OrderByDescending(r => r.RequestDate).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var request = await _context.BloodRequests.FirstOrDefaultAsync(r => r.Id == id);
            if (request == null) return NotFound();

            // Show current stock for this blood group (useful for Day 5)
            var stock = await _context.BloodStocks
                .FirstOrDefaultAsync(s => s.BloodGroup == request.BloodGroup);
            ViewData["AvailableUnits"] = stock?.Units ?? 0;

            return View(request);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PatientName,HospitalName,BloodGroup,UnitsRequired,ContactNumber")] BloodRequest request)
        {
            if (ModelState.IsValid)
            {
                request.RequestDate = DateTime.Now;
                request.Status = RequestStatus.Pending;

                _context.Add(request);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Blood request submitted successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.BloodRequests.FindAsync(id);
            if (request == null) return NotFound();

            if (request.Status != RequestStatus.Pending)
            {
                TempData["Error"] = "This request has already been processed.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var stock = await _context.BloodStocks
                .FirstOrDefaultAsync(s => s.BloodGroup == request.BloodGroup);

            if (stock == null || stock.Units < request.UnitsRequired)
            {
                TempData["Error"] =
                    $"Cannot approve. Required: {request.UnitsRequired} unit(s), available: {stock?.Units ?? 0}.";
                return RedirectToAction(nameof(Details), new { id });
            }

            stock.Units -= request.UnitsRequired;
            stock.LastUpdated = DateTime.Now;
            request.Status = RequestStatus.Approved;

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Request approved. {request.UnitsRequired} unit(s) deducted from {request.BloodGroup}.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.BloodRequests.FindAsync(id);
            if (request == null) return NotFound();

            if (request.Status != RequestStatus.Pending)
            {
                TempData["Error"] = "This request has already been processed.";
                return RedirectToAction(nameof(Details), new { id });
            }

            request.Status = RequestStatus.Rejected;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Request rejected.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
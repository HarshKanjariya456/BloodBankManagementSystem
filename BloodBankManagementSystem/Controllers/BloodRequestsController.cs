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
    }
}
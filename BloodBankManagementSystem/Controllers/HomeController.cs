using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using BloodBankManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cutoff = DateTime.Today.AddDays(-90);

            var model = new DashboardViewModel
            {
                TotalDonors = await _context.Donors.CountAsync(),
                EligibleDonors = await _context.Donors
                    .CountAsync(d => d.LastDonationDate == null || d.LastDonationDate <= cutoff),
                TotalUnits = await _context.BloodStocks.SumAsync(s => s.Units),
                PendingRequests = await _context.BloodRequests.CountAsync(r => r.Status == RequestStatus.Pending),
                ApprovedRequests = await _context.BloodRequests.CountAsync(r => r.Status == RequestStatus.Approved),
                RejectedRequests = await _context.BloodRequests.CountAsync(r => r.Status == RequestStatus.Rejected),
                Stocks = await _context.BloodStocks.OrderBy(s => s.BloodGroup).ToListAsync()
            };

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Landing()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction(nameof(Index));

            ViewData["Stocks"] = await _context.BloodStocks.OrderBy(s => s.BloodGroup).ToListAsync();
            ViewData["DonorCount"] = await _context.Donors.CountAsync();
            ViewData["FulfilledCount"] = await _context.BloodRequests
                .CountAsync(r => r.Status == RequestStatus.Approved);

            return View();
        }
    }
}

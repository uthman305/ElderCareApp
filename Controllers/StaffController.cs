using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElderCareApp.Controllers
{
   [Route("Staff")] // Base route for everything in this controller
    public class StaffController : Controller
    {
        private readonly IHealthService _healthService;
        private readonly IElderService _elderService;
        private readonly IPdfService _pdfService;

        public StaffController(IHealthService healthService, IElderService elderService, IPdfService pdfService)
        {
            _healthService = healthService;
            _elderService = elderService;
            _pdfService = pdfService;
        }

        // 1. DASHBOARD - Changed to the default "Index" action
        [HttpGet("")] // This makes 'Staff/' load the Dashboard
        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var careHomeId = HttpContext.Session.GetInt32("CareHomeId");

            if (!careHomeId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var elders = await _elderService.GetEldersByCareHomeAsync(careHomeId.Value);
            return View(elders);
        }

        // 2. GET: ADD HEALTH
        [HttpGet("AddHealth/{id}")] // Added /{id} so the route is unique
        public IActionResult AddHealth(int id)
        {
            var model = new AddHealthDto { ElderId = id }; 
            return View(model);
        }

        // 3. POST: SAVE HEALTH
        [HttpPost("AddHealth/{id?}")] // Matches the GET route
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHealth(AddHealthDto dto)
        {
            var staffId = HttpContext.Session.GetInt32("UserId");

            if (!staffId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(dto.Notes)) 
            {
                dto.Notes = "Routine checkup";
            }

            if (!ModelState.IsValid)
            {
                return View(dto); 
            }

            try 
            {
                await _healthService.AddHealthRecordAsync(dto, staffId.Value);
                return RedirectToAction(nameof(Dashboard));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save: " + ex.Message);
                return View(dto);
            }
        }

        [HttpGet("ViewHealth/{elderId}")] 
public async Task<IActionResult> ViewHealth(int elderId)
{
    var records = await _healthService.GetHealthByElderAsync(elderId);
    
    // Sort by date so the chart flows correctly
    var sortedRecords = records.OrderBy(r => r.DateRecorded).ToList();
    
    return View(sortedRecords);
}
[HttpGet("DownloadReport/{elderId}")]
public async Task<IActionResult> DownloadReport(int elderId)
{
    // 1. THIS IS THE MISTAKE: 
    // You were likely calling GetEldersByCareHomeAsync(elderId) 
    // which returns a LIST of everyone. .FirstOrDefault() then just 
    // grabs the very first person in the DB (Idris Elba).

    // FIX: Call the specific ID method
    var elderList = await _elderService.GetByIdAsync(elderId); 
    var elder = elderList?.FirstOrDefault();

    if (elder == null)
    {
        return NotFound($"Could not find Elder with ID: {elderId}");
    }

    // 2. Ensure your Health Records are also filtered by this specific ID
    var records = await _healthService.GetHealthByElderAsync(elderId);

    // 3. Generate the PDF
    var pdfBytes = _pdfService.GenerateHealthReport(elder, records);
    
    return File(pdfBytes, "application/pdf", $"HealthReport_{elder.FullName.Replace(" ", "_")}.pdf");
}
    }
}
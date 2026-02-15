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
    // [Route("[controller]")]
    [Route("Staff")]
    public class StaffController : Controller
    {
       private readonly IHealthService _healthService;
         private readonly IElderService _elderService;

    public StaffController(IHealthService healthService,   IElderService elderService)
    {
        _healthService = healthService;
        _elderService = elderService;
    }
[HttpGet]
    public IActionResult AddHealth(int elderId)
    {
        if (HttpContext.Session.GetString("Role") != "Staff")
            return RedirectToAction("Login", "Account");

        return View(new AddHealthDto { ElderId = elderId });
    }

    // ✅ SAVE FORM
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddHealth(AddHealthDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var staffId = HttpContext.Session.GetInt32("StaffId");
        if (staffId == null)
            return RedirectToAction("Login", "Account");

        await _healthService.AddHealthRecordAsync(dto, staffId.Value);

        return RedirectToAction("Dashboard", "Staff");
    }
     [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        if (HttpContext.Session.GetString("Role") != "Staff")
            return RedirectToAction("Login", "Account");

        var careHomeId = HttpContext.Session.GetInt32("CareHomeId");
        var elders = await _elderService.GetByCareHomeIdAsync(careHomeId.Value);

        return View(elders);
    }
   
[HttpGet("Staff/ViewHealth/{elderId}")] 
public async Task<IActionResult> ViewHealth(int elderId)
{
    if (HttpContext.Session.GetString("Role") != "Staff" &&
        HttpContext.Session.GetString("Role") != "Manager")
        return RedirectToAction("Login", "Account");

    var records = await _healthService.GetHealthByElderAsync(elderId);
    return View(records);
}

    }
}
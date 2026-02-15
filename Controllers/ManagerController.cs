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
    
    public class ManagerController : Controller
    {
        private readonly IElderService _elderService;
         private readonly IStaffService _staffService;
         private readonly IHealthService _healthService;

    public ManagerController(IElderService elderService, IStaffService staffService, IHealthService healthService)
    {
        _elderService = elderService;
        _staffService = staffService;
        _healthService = healthService;

    }

    public async Task<IActionResult> Dashboard()
    {
        var careHomeId = HttpContext.Session.GetInt32("CareHomeId");
        if (careHomeId == null)
            return RedirectToAction("Login", "Account");

        var elders = await _elderService.GetByCareHomeIdAsync(careHomeId.Value);
        return View(elders);
    }

    public async Task<IActionResult> RemoveElder(int id)
    {
        await _elderService.RemoveAsync(id);
        return RedirectToAction(nameof(Dashboard));
    }

      public IActionResult CreateStaff()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateStaff(CreateStaffDto dto)
    {
        var careHomeId = HttpContext.Session.GetInt32("CareHomeId");

        await _staffService.CreateStaffAsync(dto, careHomeId.Value);

        return RedirectToAction("Dashboard");
    }
    public async Task<IActionResult> ViewHealth(int elderId)
{
    if (HttpContext.Session.GetString("Role") != "Manager")
        return RedirectToAction("Login", "Account");

    var records = await _healthService.GetHealthByElderAsync(elderId);
    ViewBag.ElderId = elderId;

    return View(records);
}

}
}
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
    
    public class ManagerController : SecureController
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
[HttpGet]
public async Task<IActionResult> Dashboard()
{
    // 1. Check if UserId actually has a value before using .Value
    if (!UserId.HasValue)
    {
        // If it's null, the session expired or login failed. Send them back.
        return RedirectToAction("Login", "Account");
    }

    var careHomeId = UserId.Value; 
    var elders = await _elderService.GetEldersByCareHomeAsync(careHomeId);
    return View(elders);
}
[HttpPost]
    public async Task<IActionResult> RemoveElder(int id)
    {
        await _elderService.RemoveAsync(id);
        return RedirectToAction(nameof(Dashboard));
    }
[HttpGet]
      public IActionResult CreateStaff()
    {
        return View();
    }

   [HttpPost]
public async Task<IActionResult> CreateStaff(CreateStaffDto dto)
{
    // 1. Get the ID from our SecureController helper
    var careHomeId = UserId; 

    // 2. SAFETY CHECK: If the session is gone, we can't save the staff
    if (!careHomeId.HasValue)
    {
        // Log them out or send to login because we don't know which CareHome they belong to
        return RedirectToAction("Login", "Account");
    }

    if (!ModelState.IsValid) 
    {
        return View(dto);
    }

    // 3. Use the value safely
    await _staffService.CreateStaffAsync(dto, careHomeId.Value);
    
    return RedirectToAction(nameof(Dashboard));
}
    [HttpGet]
    public async Task<IActionResult> ViewHealth(int elderId)
{

    var records = await _healthService.GetHealthByElderAsync(elderId);
    ViewBag.ElderId = elderId;

    return View(records);
}
[HttpGet]
public async Task<IActionResult> ElderDetails(int id)
{
    var healthRecords = await _healthService.GetHealthByElderAsync(id);
    // Sort records by date so the chart flows correctly
    var sortedRecords = healthRecords.OrderBy(r => r.DateRecorded).ToList();
    
    return View(sortedRecords);
}
}
}
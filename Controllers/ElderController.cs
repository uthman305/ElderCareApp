using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Interfaces.Services;
using ElderCareApp.Models;
using ElderCareApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ElderCareApp.Controllers
{
    [Route("[controller]")]
   public class ElderController : Controller
{
    private readonly IElderService _elderService;
    private readonly ICareHomeService _careHomeService;

    public ElderController(
        IElderService elderService,
        ICareHomeService careHomeService)
    {
        _elderService = elderService;
        _careHomeService = careHomeService;
    }

    [HttpGet]
public async Task<IActionResult> Enroll()
{
    // 1. Check if a CareHomeId exists in the session (Staff/Manager is logged in)
    var sessionCareHomeId = HttpContext.Session.GetInt32("CareHomeId");
    
    // 2. Prepare the list of Care Homes (Only needed for Guests)
    var careHomes = await _careHomeService.GetAllCareHomesAsync();
    ViewBag.CareHomeList = new SelectList(careHomes, "Id", "Name");

    var model = new EnrollElderDto();

    if (sessionCareHomeId.HasValue)
    {
        // Pre-set the ID so the model knows where it belongs
        model.CareHomeId = sessionCareHomeId.Value;
        
        // Pass a flag to the View to hide the dropdown
        ViewBag.IsUserLoggedIn = true;
        ViewBag.CurrentCareHomeName = HttpContext.Session.GetString("CareHomeName") ?? "Your Assigned Home";
    }
    else
    {
        ViewBag.IsUserLoggedIn = false;
    }

    return View(model);
}
 
  [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Enroll(EnrollElderDto dto)
{
    var sessionCareHomeId = HttpContext.Session.GetInt32("CareHomeId");

    // CRITICAL SECURITY: If logged in, ignore whatever ID came from the form
    if (sessionCareHomeId.HasValue)
    {
        dto.CareHomeId = sessionCareHomeId.Value;
    }

    if (!ModelState.IsValid)
    {
        // Re-populate ViewBag if we have to return to the view due to errors
        var careHomes = await _careHomeService.GetAllCareHomesAsync();
        ViewBag.CareHomeList = new SelectList(careHomes, "Id", "Name");
        ViewBag.IsUserLoggedIn = sessionCareHomeId.HasValue;
        return View(dto);
    }

    await _elderService.EnrollElderAsync(dto);
    
    // Redirect based on user type
    if (sessionCareHomeId.HasValue) 
        return RedirectToAction("Dashboard", "Staff");
    
    return RedirectToAction("Success", "Public");
}
}


}
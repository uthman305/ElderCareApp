using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElderCareApp.Controllers
{
    
    public class ManagerController : Controller
    {
        private readonly IElderService _elderService;

    public ManagerController(IElderService elderService)
    {
        _elderService = elderService;
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
}
}
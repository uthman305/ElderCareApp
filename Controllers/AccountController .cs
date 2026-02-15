using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Implementations.Services;
using ElderCareApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElderCareApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly IStaffService _staffService;

        public AccountController(AuthService authService, IStaffService staffService)
        {
            _authService = authService;
            _staffService = staffService;
        }




       public IActionResult Login()
{
    return View(new LoginDto());
}

[HttpPost]
public async Task<IActionResult> Login(LoginDto dto)
{
    if (!ModelState.IsValid)
        return View(dto);

    // Try Manager login
    var manager = await _authService.ManagerLoginAsync(dto.Email, dto.Password);
    if (manager != null)
    {
        HttpContext.Session.SetString("Role", "Manager");
        HttpContext.Session.SetInt32("CareHomeId", manager.Id);
        HttpContext.Session.SetString("ManagerName", manager.ManagerName);

        return RedirectToAction("Dashboard", "Manager");
    }

    // Try Staff login
    var staff = await _authService.StaffLoginAsync(dto.Email, dto.Password);
    if (staff != null)
    {
        HttpContext.Session.SetString("Role", "Staff");
        HttpContext.Session.SetInt32("StaffId", staff.Id);
        HttpContext.Session.SetInt32("CareHomeId", staff.CareHomeId);

        return RedirectToAction("Dashboard", "Staff");
    }

    ModelState.AddModelError("", "Invalid login credentials");
    return View(dto);
}

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElderCareApp.Controllers
{
   
    public class AccountController  : Controller
    {
        private readonly AuthService _authService;

    public AccountController(AuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(ManagerLoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var careHome = await _authService.LoginAsync(dto.Email, dto.Password);

        if (careHome == null)
        {
            ModelState.AddModelError("", "Invalid login credentials");
            return View(dto);
        }

        HttpContext.Session.SetInt32("CareHomeId", careHome.Id);
        HttpContext.Session.SetString("ManagerName", careHome.ManagerName);

        return RedirectToAction("Dashboard", "Manager");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
}
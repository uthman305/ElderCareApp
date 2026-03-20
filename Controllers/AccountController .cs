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




      [HttpGet]
        public IActionResult Login()
        {
            // If user is already logged in, send them to their dashboard
            var role = HttpContext.Session.GetString("Role");
            if (role == "Manager") return RedirectToAction("Dashboard", "Manager");
            if (role == "Staff") return RedirectToAction("Index", "Staff");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            // 1. Call the unified LoginAsync that checks both tables
            var sessionUser = await _authService.LoginAsync(dto.Email, dto.Password);

            if (sessionUser == null)
            {
                // 2. Add error if login fails
                ModelState.AddModelError(string.Empty, "Invalid Email or Password.");
                return View(dto);
            }

            // 3. Set Session variables (This is what SecureController looks for)
            HttpContext.Session.SetInt32("UserId", sessionUser.Id);
            HttpContext.Session.SetString("Role", sessionUser.Role);
            HttpContext.Session.SetInt32("CareHomeId", sessionUser.CareHomeId); 

            // 4. Redirect based on the Role returned by the Service
            if (sessionUser.Role == "Manager")
            {
                return RedirectToAction("Dashboard", "Manager");
            }
            else
            {
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    

    }
}
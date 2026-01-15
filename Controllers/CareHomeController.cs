using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Interfaces.Services;
using ElderCareApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElderCareApp.Controllers
{
    
    public class CareHomeController : Controller
{
    private readonly ICareHomeService _careHomeService;

    public CareHomeController(ICareHomeService careHomeService)
    {
        _careHomeService = careHomeService;
    }

    public async Task<IActionResult> Index()
    {
        var careHomes = await _careHomeService.GetAllCareHomesAsync();
        return View(careHomes);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

   
    [HttpPost]
public async Task<IActionResult> Register(RegisterCareHomeDto dto)
{
    if (!ModelState.IsValid)
        return View(dto);

    try
    {
        await _careHomeService.RegisterCareHomeAsync(dto);
        return RedirectToAction(nameof(Index));
    }
    catch (InvalidOperationException ex)
    {
        ModelState.AddModelError("", ex.Message);
        return View(dto);
    }
}

}

}
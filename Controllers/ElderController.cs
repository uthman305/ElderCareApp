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
        var careHomes = await _careHomeService.GetAllCareHomesAsync();

        var model = new EnrollElderViewModel
        {
            Elder = new EnrollElderDto(),
            CareHomes = careHomes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
        };

        return View(model);
    }

 
    [HttpPost]
public async Task<IActionResult> Enroll(EnrollElderViewModel model)
{
    if (!ModelState.IsValid)
    {
        foreach (var entry in ModelState)
        {
            foreach (var error in entry.Value.Errors)
            {
                Console.WriteLine($"FIELD: {entry.Key} - ERROR: {error.ErrorMessage}");
            }
        }

        var careHomes = await _careHomeService.GetAllCareHomesAsync();
        model.CareHomes = careHomes.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        });

        return View(model);
    }

    await _elderService.EnrollElderAsync(model.Elder);
    return RedirectToAction("Index", "CareHome");
}

}


}
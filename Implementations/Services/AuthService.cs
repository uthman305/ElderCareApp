using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Helpers;
using ElderCareApp.Interfaces.Repositories;
using ElderCareApp.Models;

namespace ElderCareApp.Implementations.Services
{
    public class AuthService
    {
    //     private readonly ICareHomeRepository _repo;

    // public AuthService(ICareHomeRepository repo)
    // {
    //     _repo = repo;
    // }

    // public async Task<CareHome?> LoginAsync(string email, string password)
    // {
    //     var careHome = await _repo.GetByManagerEmailAsync(email);
    //     if (careHome == null) return null;

    //     var hash = PasswordHelper.HashPassword(password);
    //     return careHome.PasswordHash == hash ? careHome : null;
    // }
    private readonly ICareHomeRepository _careHomeRepo;
    private readonly IStaffRepository _staffRepo;

    public AuthService(
        ICareHomeRepository careHomeRepo,
        IStaffRepository staffRepo)
    {
        _careHomeRepo = careHomeRepo;
        _staffRepo = staffRepo;
    }

    
        // // 1️⃣ Check manager login
        // public async Task<CareHome?> ManagerLoginAsync(string email, string password)
        // {
        //     var careHome = await _careHomeRepo.GetByManagerEmailAsync(email);

        //     if (careHome == null)
        //         return null;

        //     // plain password comparison (as you requested)
        //     if (careHome.PasswordHash != password)
        //         return null;

        //     return careHome;
        // }

        // // 2️⃣ Check staff login
        // public async Task<Staff?> StaffLoginAsync(string email, string password)
        // {
        //     var staff = await _staffRepo.GetByEmailAsync(email);

        //     if (staff == null)
        //         return null;

        //     if (staff.Password != password)
        //         return null;

        //     return staff;
        // }

      public async Task<UserSessionDto?> LoginAsync(string email, string password)
    {
        // 1. Try to find the user in the CareHome (Manager) table first
        var manager = await _careHomeRepo.GetByManagerEmailAsync(email);
        if (manager != null)
        {
            // Check password (using plain text bypass for now as discussed)
            if (manager.PasswordHash == password || manager.PasswordHash == PasswordHelper.HashPassword(password))
            {
                return new UserSessionDto
                {
                    Id = manager.Id,
                    FullName = manager.Name, // CareHome has a Name property
                    Role = "Manager",
                    CareHomeId = manager.Id
                };
            }
        }

        // 2. If not found in Managers, try the Staff table
        var staff = await _staffRepo.GetByEmailAsync(email);
        if (staff != null)
        {
            if (staff.Password == password || staff.Password == PasswordHelper.HashPassword(password))
            {
                return new UserSessionDto
                {
                    Id = staff.Id,
                    FullName = staff.FullName,
                    Role = "Staff",
                    CareHomeId = staff.CareHomeId
                };
            }
        }

        // 3. If neither matches, return null
        return null;
    }
    }
}
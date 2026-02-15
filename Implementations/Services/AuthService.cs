using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    
        // 1️⃣ Check manager login
        public async Task<CareHome?> ManagerLoginAsync(string email, string password)
        {
            var careHome = await _careHomeRepo.GetByManagerEmailAsync(email);

            if (careHome == null)
                return null;

            // plain password comparison (as you requested)
            if (careHome.PasswordHash != password)
                return null;

            return careHome;
        }

        // 2️⃣ Check staff login
        public async Task<Staff?> StaffLoginAsync(string email, string password)
        {
            var staff = await _staffRepo.GetByEmailAsync(email);

            if (staff == null)
                return null;

            if (staff.Password != password)
                return null;

            return staff;
        }
    }
}
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
        private readonly ICareHomeRepository _repo;

    public AuthService(ICareHomeRepository repo)
    {
        _repo = repo;
    }

    public async Task<CareHome?> LoginAsync(string email, string password)
    {
        var careHome = await _repo.GetByManagerEmailAsync(email);
        if (careHome == null) return null;

        var hash = PasswordHelper.HashPassword(password);
        return careHome.PasswordHash == hash ? careHome : null;
    }
    }
}
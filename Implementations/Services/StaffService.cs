using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Interfaces.Repositories;
using ElderCareApp.Interfaces.Services;
using ElderCareApp.Models;

namespace ElderCareApp.Implementations.Services
{
    public class StaffService : IStaffService
    {
    private readonly IStaffRepository _repo;

    public StaffService(IStaffRepository repo)
    {
        _repo = repo;
    }

    public async Task CreateStaffAsync(CreateStaffDto dto, int careHomeId)
    {
        var staff = new Staff
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Password = dto.Password,
            CareHomeId = careHomeId,
            Role = "Staff"
        };

        await _repo.AddAsync(staff);
    }

    public async Task<Staff?> LoginAsync(string email, string password)
    {
        var staff = await _repo.GetByEmailAsync(email);

        if (staff == null || staff.Password != password)
            return null;

        return staff;
    }

    public async Task<IEnumerable<Staff>> GetStaffByCareHomeAsync(int careHomeId)
    {
        return await _repo.GetByCareHomeAsync(careHomeId);
    }

        
    }
}
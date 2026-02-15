using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Data;
using ElderCareApp.Interfaces.Repositories;
using ElderCareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ElderCareApp.Implementations.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        
    private readonly ApplicationDbContext _context;

    public StaffRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Staff staff)
    {
        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync();
    }

    public async Task<Staff?> GetByEmailAsync(string email)
    {
        return await _context.Staffs
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<IEnumerable<Staff>> GetByCareHomeAsync(int careHomeId)
    {
        return await _context.Staffs
            .Where(s => s.CareHomeId == careHomeId)
            .ToListAsync();
    }
    }
}
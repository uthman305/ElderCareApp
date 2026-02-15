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
    public class HealthRepository : IHealthRepository
    {
      private readonly ApplicationDbContext _context;

    public HealthRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ElderHealthRecord record)
    {
        _context.ElderHealthRecords.Add(record);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ElderHealthRecord>> GetByElderAsync(int elderId)
    {
        return await _context.ElderHealthRecords
            .Where(h => h.ElderId == elderId)
            .Include(h => h.Staff)
            .ToListAsync();
    }
}  
    }

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
    public class ElderRepository : IElderRepository
    {
        private readonly ApplicationDbContext _context;

        public ElderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Elder>> GetByCareHomeIdAsync(int careHomeId)
        {
            return await _context.Elders
                .Where(e => e.CareHomeId == careHomeId)
                .ToListAsync();
        }
        public async Task<Elder?> GetByIdAsync(int id)
    {
        return await _context.Elders
            .FirstOrDefaultAsync(e => e.Id == id);
    }

        public async Task AddAsync(Elder elder)
        {
            _context.Elders.Add(elder);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(Elder elder)
        {
            _context.Elders.Remove(elder);
            await _context.SaveChangesAsync();
        }
    }

}
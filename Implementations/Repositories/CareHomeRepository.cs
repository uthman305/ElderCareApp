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
    public class CareHomeRepository : ICareHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public CareHomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CareHome>> GetAllAsync()
        {
            return await _context.CareHomes.ToListAsync();
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.CareHomes
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
        public async Task<CareHome?> GetByManagerEmailAsync(string email)
        {
            return await _context.CareHomes
                .FirstOrDefaultAsync(c => c.ManagerEmail == email);
        }

        public async Task<CareHome> GetByIdAsync(int id)
        {
            return await _context.CareHomes.FindAsync(id);
        }

        public async Task AddAsync(CareHome careHome)
        {
            _context.CareHomes.Add(careHome);
            await _context.SaveChangesAsync();
        }
    }
}
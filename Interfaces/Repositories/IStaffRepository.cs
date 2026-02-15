using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Repositories
{
    public interface IStaffRepository
    {
        Task AddAsync(Staff staff);
    Task<Staff?> GetByEmailAsync(string email);
    Task<IEnumerable<Staff>> GetByCareHomeAsync(int careHomeId);
    }
}
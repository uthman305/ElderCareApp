using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Repositories
{
    public interface ICareHomeRepository
    {
        Task<IEnumerable<CareHome>> GetAllAsync();
    Task<CareHome> GetByIdAsync(int id);
    Task AddAsync(CareHome careHome);
Task<bool> ExistsByNameAsync(string name);
Task<CareHome?> GetByManagerEmailAsync(string email);


    }
}
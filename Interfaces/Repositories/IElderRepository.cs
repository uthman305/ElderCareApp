using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Repositories
{
    public interface IElderRepository
    {
        Task<IEnumerable<Elder>> GetByCareHomeIdAsync(int careHomeId);
        Task AddAsync(Elder elder);
            Task<Elder?> GetByIdAsync(int id);
        Task RemoveAsync(Elder elder);
    } 
}
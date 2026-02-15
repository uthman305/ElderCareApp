using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Repositories
{
    public interface IHealthRepository
    {
        Task AddAsync(ElderHealthRecord record);
    Task<IEnumerable<ElderHealthRecord>> GetByElderAsync(int elderId);
    }
}
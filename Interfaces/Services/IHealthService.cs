using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Services
{
    public interface IHealthService
    {
        Task AddHealthRecordAsync(AddHealthDto dto, int staffId);
    Task<IEnumerable<ElderHealthRecord>> GetHealthByElderAsync(int elderId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Services
{
    public interface IElderService
    {
        Task EnrollElderAsync(EnrollElderDto dto);
        Task<IEnumerable<Elder>> GetEldersByCareHomeAsync(int careHomeId);
        Task<IEnumerable<Elder>> GetByCareHomeIdAsync(int careHomeId);
        Task RemoveAsync(int elderId);
    }
}
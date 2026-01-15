using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Services
{
    public interface ICareHomeService
    {
        Task<IEnumerable<CareHome>> GetAllCareHomesAsync();
Task RegisterCareHomeAsync(RegisterCareHomeDto dto);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Services
{
    public interface IStaffService
    {
         Task CreateStaffAsync(CreateStaffDto dto, int careHomeId);
    Task<Staff?> LoginAsync(string email, string password);
    Task<IEnumerable<Staff>> GetStaffByCareHomeAsync(int careHomeId);
    }
}
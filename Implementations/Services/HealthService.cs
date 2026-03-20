using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Data; // 👈 Required to see ApplicationDbContext
using ElderCareApp.Dto;
using ElderCareApp.Interfaces.Repositories;
using ElderCareApp.Interfaces.Services;
using ElderCareApp.Models;
using Microsoft.EntityFrameworkCore; // 👈 Required for EF operations

namespace ElderCareApp.Implementations.Services
{
    public class HealthService : IHealthService
    {
        private readonly IHealthRepository _repo;
        private readonly ApplicationDbContext _context; // 👈 1. Declare the field

        // 2. Add context to the Constructor parameters
        public HealthService(IHealthRepository repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context; // 👈 3. Assign it
        }

        public async Task AddHealthRecordAsync(AddHealthDto dto, int staffId)
        {
            // First, save the health record as usual
            var record = new ElderHealthRecord
            {
                ElderId = dto.ElderId,
                BloodPressure = dto.BloodPressure,
                HeartRate = dto.HeartRate,
                Notes = dto.Notes,
                StaffId = staffId
            };
            await _repo.AddAsync(record);

            // ADVANCED LOGIC: Update Elder Status based on the new data
            var elder = await _context.Elders.FindAsync(dto.ElderId);
            if (elder != null)
            {
                // Logic: High heart rate (>100) or low (<50) is Critical
                if (dto.HeartRate > 100 || dto.HeartRate < 50)
                {
                    elder.Status = "Critical";
                }
                else if (dto.HeartRate > 90 || dto.HeartRate < 60)
                {
                    elder.Status = "Monitor";
                }
                else
                {
                    elder.Status = "Stable";
                }

                // Save the changes to the Elder table
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ElderHealthRecord>> GetHealthByElderAsync(int elderId)
        {
            return await _repo.GetByElderAsync(elderId);
        }
    }
}
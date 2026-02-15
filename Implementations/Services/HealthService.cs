using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Interfaces.Repositories;
using ElderCareApp.Interfaces.Services;
using ElderCareApp.Models;

namespace ElderCareApp.Implementations.Services
{
    public class HealthService : IHealthService
    {
        
   private readonly IHealthRepository _repo;

    public HealthService(IHealthRepository repo)
    {
        _repo = repo;
    }

    public async Task AddHealthRecordAsync(AddHealthDto dto, int staffId)
    {
        var record = new ElderHealthRecord
        {
            ElderId = dto.ElderId,
            BloodPressure = dto.BloodPressure,
            HeartRate = dto.HeartRate,
            Notes = dto.Notes,
            StaffId = staffId
        };

        await _repo.AddAsync(record);
    }

    public async Task<IEnumerable<ElderHealthRecord>> GetHealthByElderAsync(int elderId)
    {
        return await _repo.GetByElderAsync(elderId);
    }
}
}
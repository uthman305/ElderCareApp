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
    public class ElderService : IElderService
{
    private readonly IElderRepository _elderRepository;
    private readonly ICareHomeRepository _careHomeRepository;

    public ElderService(
        IElderRepository elderRepository,
        ICareHomeRepository careHomeRepository)
    {
        _elderRepository = elderRepository;
        _careHomeRepository = careHomeRepository;
    }

   public async Task EnrollElderAsync(EnrollElderDto dto)
{

    var careHome = await _careHomeRepository.GetByIdAsync(dto.CareHomeId);
    if (careHome == null)
        throw new Exception("Invalid care home");

    var elder = new Elder
    {
        FullName = dto.FullName,
        Age = dto.Age,
        CareHomeId = dto.CareHomeId
    };

    await _elderRepository.AddAsync(elder);
}

        public async Task<IEnumerable<Elder>> GetByIdAsync(int id)
        {
         return await _elderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Elder>> GetEldersByCareHomeAsync(int careHomeId)
    {
        return await _elderRepository.GetByCareHomeIdAsync(careHomeId);
    }
    

  
    public async Task RemoveAsync(int elderId)
{
    var elderList = await _elderRepository.GetByIdAsync(elderId);
    var elder = elderList?.FirstOrDefault();
    if (elder != null)
    {
        await _elderRepository.RemoveAsync(elder);
    }
}
}

}
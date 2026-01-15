using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using ElderCareApp.Helpers;
using ElderCareApp.Interfaces.Repositories;
using ElderCareApp.Interfaces.Services;
using ElderCareApp.Models;

namespace ElderCareApp.Implementations.Services
{
    public class CareHomeService : ICareHomeService
    {
        private readonly ICareHomeRepository _careHomeRepository;

    public CareHomeService(ICareHomeRepository careHomeRepository)
    {
        _careHomeRepository = careHomeRepository;
    }

    public async Task<IEnumerable<CareHome>> GetAllCareHomesAsync()
    {
        return await _careHomeRepository.GetAllAsync();
    }

   
   public async Task RegisterCareHomeAsync(RegisterCareHomeDto dto)
{
    if (await _careHomeRepository.ExistsByNameAsync(dto.Name))
        throw new InvalidOperationException("Care home already exists");

    var careHome = new CareHome
    {
        Name = dto.Name,
        Address = dto.Address,
        BankName = dto.BankName,
        AccountNumber = dto.AccountNumber,
        ManagerName = dto.ManagerName,
        ManagerEmail = dto.ManagerEmail,
        PasswordHash = PasswordHelper.HashPassword(dto.Password)
    };

    await _careHomeRepository.AddAsync(careHome);
}


}
    }

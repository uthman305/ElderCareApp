using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Models;

namespace ElderCareApp.Interfaces.Services
{
    public interface IPdfService
    {
        byte[] GenerateHealthReport(Elder elder, IEnumerable<ElderHealthRecord> records);
    }
}
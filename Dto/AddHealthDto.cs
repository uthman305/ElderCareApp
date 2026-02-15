using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElderCareApp.Dto
{
    public class AddHealthDto
    {
         public int ElderId { get; set; }
    public string BloodPressure { get; set; }
    public int HeartRate { get; set; }
    public string Notes { get; set; }
    }
}
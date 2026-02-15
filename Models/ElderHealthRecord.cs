using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElderCareApp.Models
{
    public class ElderHealthRecord
    {
         public int Id { get; set; }

    public int ElderId { get; set; }
    public Elder Elder { get; set; }

    public string BloodPressure { get; set; }
    public int HeartRate { get; set; }
    public string Notes { get; set; }

    public DateTime DateRecorded { get; set; } = DateTime.Now;

    public int StaffId { get; set; }
    public Staff Staff { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElderCareApp.Models
{
    public class Staff
    {
        public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string Role { get; set; } 

    public int CareHomeId { get; set; }
    public CareHome CareHome { get; set; }
    }
}
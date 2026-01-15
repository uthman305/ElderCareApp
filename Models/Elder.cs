using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElderCareApp.Models
{
    public class Elder
    {
        public int Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }

    public int CareHomeId { get; set; }
    public CareHome CareHome { get; set; }
    }
}
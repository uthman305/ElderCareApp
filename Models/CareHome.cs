using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElderCareApp.Models
{
    public class CareHome
    {
      public int Id { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }

    public string BankName { get; set; }
    public string AccountNumber { get; set; }

    // Manager info
    public string ManagerName { get; set; }
    public string ManagerEmail { get; set; }

    // 🔐 Auth
    public string PasswordHash { get; set; }

    public ICollection<Elder> Elders { get; set; }

    }
}
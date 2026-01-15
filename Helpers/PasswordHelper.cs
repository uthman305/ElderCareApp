using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElderCareApp.Helpers
{
    public class PasswordHelper
    {
         public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(sha256.ComputeHash(bytes));
    }
    }
}
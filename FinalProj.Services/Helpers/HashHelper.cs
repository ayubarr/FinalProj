using System.Security.Cryptography;
using System.Text;

namespace FinalApp.Services.Helpers
{
    public static class HashHelper
    {
        private static string Solt = "PowerOfTheSolt";
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashhedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + Solt));
                var hash = BitConverter.ToString(hashhedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}

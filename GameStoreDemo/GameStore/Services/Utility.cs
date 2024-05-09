using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace GameStore.Services
{
    public class Utility
    {
        public static string ConvertSHA256(string text)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                foreach (byte bt in hashValue)
                {
                    hash += $"{bt:X2}";
                }
            }

            return hash;
        }

        public static string GenerarToken()
        {
            string token = Guid.NewGuid().ToString("N");

            return token;
        }
    }
}

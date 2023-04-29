using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace EndoscopicSystem.Helpers
{
    public static class EncriptHelper
    {
        public static string HashPassword(string password)
        {
            string hashKey = ConfigurationManager.AppSettings["HashKey"];

            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hashKey)))
            {
                byte[] bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a string and return it
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Check if a password matches a stored hash
        public static bool VerifyPassword(string password, string hash)
        {
            string hashedPassword = HashPassword(password);
            return hash.Equals(hashedPassword);
        }
    }
}
using System.Security.Cryptography;
using System.Text;

namespace GnomeRides.Utils
{
    internal class Sha256Hash
    {
        public static string createHash(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static bool compareValueToHash(string value, string hash)
        {
            return createHash(value).Equals(hash);
        }
    }
}

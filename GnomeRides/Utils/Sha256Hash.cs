using System.Security.Cryptography;
using System.Text;

namespace GnomeRides.Utils
{
    internal class Sha256Hash
    {
        /// <summary>
        /// Create a Sha256 hash
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A the input string as a Sha256 hash string</returns>
        public static string CreateHash(string data)
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

        /// <summary>
        /// Compare a string to a hash value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="hash"></param>
        /// <returns>True if the value is the hashed value</returns>
        public static bool CompareValueToHash(string value, string hash)
        {
            return CreateHash(value).Equals(hash);
        }
    }
}

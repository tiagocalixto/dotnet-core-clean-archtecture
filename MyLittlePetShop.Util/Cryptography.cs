using System.Security.Cryptography;
using System.Text;

namespace MyLittlePetShop.Util
{
    public static class Cryptography
    {
        public static string HashString(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value, 4);
        }

        public static bool Verify(string value, string encryptedValue)
        {
            return BCrypt.Net.BCrypt.Verify(value, encryptedValue);
        }
        
        public static string HashStringMd5(string value)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();

            foreach (var character in data)
            {
                sBuilder.Append(character.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string HashStringMd5MultipleTimes(string value, int times)
        {
            for (int i = 0; i < times; i++)
            {
                value = HashStringMd5(value);
            }

            return value;
        }
    }
}
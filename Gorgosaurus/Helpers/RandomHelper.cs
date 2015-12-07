using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Helpers
{
    public static class RandomHelper
    {
        public static string GetRandomAlphanumericString(int length)
        {
            const string possibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] chars = new char[possibleChars.Length];
            chars = possibleChars.ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[length];
                crypto.GetNonZeroBytes(data);
            }
            var result = new StringBuilder(length);
            foreach (byte randomByte in data)
            {
                result.Append(chars[randomByte % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}

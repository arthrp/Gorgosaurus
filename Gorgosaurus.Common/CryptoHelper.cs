using CryptSharp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Common
{
    public static class CryptoHelper
    {
        //TODO: find optimal one
        private const int RES_COST = 32768;

        public static string GenerateHash(string passPlain, string salt)
        {
            byte[] passBytes = Encoding.Unicode.GetBytes(passPlain);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);

            byte[] res = SCrypt.ComputeDerivedKey(passBytes, saltBytes, RES_COST, 8, 1, null, 256);
            string resPassword = Convert.ToBase64String(res);

            return resPassword;
        }
    }
}

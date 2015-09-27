using Dapper;
using Gorgosaurus.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA
{
    public class AccountChecker
    {
        public const int SALT_LENGTH = 5;

        //TODO: use DI
        public static readonly AccountChecker Instance = new AccountChecker();

        public bool AreCredentialsValid(string username, string passwordPlain)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                string currentPass = conn.Query<string>("select Password from ForumUser where Username = :username",
                    new { username = username }).FirstOrDefault();

                if (String.IsNullOrEmpty(currentPass))
                    throw new ArgumentException("User not found");

                string salt = currentPass.Substring(0, SALT_LENGTH);
                string dbPaswordHash = currentPass.Substring(SALT_LENGTH);

                string generatedHash = CryptoHelper.GenerateHash(passwordPlain, salt);
                var isValid = dbPaswordHash.ToCharArray().SequenceEqual(generatedHash.ToCharArray());

                return isValid;

                //int users = conn.Query<int>("select count(*) from ForumUser where Username = :username and Password = :password",
                //    new { username = username, password = passwordPlain }).FirstOrDefault();

                //return users > 0;
            }
        }
    }
}

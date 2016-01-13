using Dapper;
using Gorgosaurus.BO.Entities;
using Gorgosaurus.Common;
using Gorgosaurus.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Managers
{
    public class AccountManager
    {
        public const int SALT_LENGTH = 5;

        //TODO: use DI
        public static readonly AccountManager Instance = new AccountManager();

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
            }
        }

        public void CreateUser(ForumUser user)
        {
            string salt = DateTime.UtcNow.ToString("ddMMmm").Substring(0, SALT_LENGTH);
            string hash = CryptoHelper.GenerateHash(user.Password, salt);

            user.Password = salt + hash;

            UserRepository.Instance.Insert(user, skipId: true);
        }


        public IEnumerable<ForumUser> GetAllUsers()
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var res = conn.Query<ForumUser>(
                    @"select * from ForumUser"
                );

                return res;
            }
        }
    }
}

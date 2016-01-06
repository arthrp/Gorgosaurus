using Gorgosaurus.BO.Entities;
using Gorgosaurus.Common;
using Gorgosaurus.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.IntegrationTests
{
    public abstract class BaseAccountTests
    {
        protected void CreateUser(string username, string password, long id = 1)
        {
            string userSalt = "11111";

            string hash = CryptoHelper.GenerateHash(password, userSalt);
            UserRepository.Instance.Insert(new ForumUser()
            {
                Id = id,
                Username = username,
                Password = userSalt + hash,
                IsAdmin = true,
                Name = "Tester",
                Surname = "Testerson"
            });
        }
    }
}

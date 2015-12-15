using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.IntegrationTests
{
    [TestFixture]
    public class ForumUserTests
    {
        [SetUp]
        public void Setup()
        {
            DbConnector.Delete();
            DbConnector.Init();
        }

        [Test]
        public void AddingUserWithNonUniqueNameFails()
        {
            const string username = "johny";
            var user = new ForumUser() { Id = 1, IsAdmin = false, Name = "John", Surname = "Smith", Username = username, Password = "123" };

            UserRepository.Instance.Insert(user);
            var dbUser = UserRepository.Instance.Get(1);

            Assert.IsNotNull(dbUser);
            Assert.True(dbUser.Username.Equals(username));

#if __MonoCS__
            Assert.Throws<Mono.Data.Sqlite.SqliteException>(() => SubforumRepository.Instance.Insert(user, true));
#else
            Assert.Throws<SQLiteException>(() => UserRepository.Instance.Insert(user, true));
#endif
        }
    }
}

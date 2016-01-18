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
    public class DiscussionTests
    {
        private const long TestUserId = 1;

        [SetUp]
        public void Setup()
        {
            DbConnector.Delete();
            DbConnector.Init();

            UserRepository.Instance.Insert(new ForumUser() { Id = TestUserId, Name = "Test", IsAdmin = false, Surname = "Smith", Username = "John", Password = "123" });
        }

        [Test]
        public void CanAddDiscussion()
        {
            const long subforumId = 1;
            const long discussionId = 1;
            const string title = "My discussion";

            SubforumRepository.Instance.Insert(new Subforum() { Id = subforumId, Title = "Test subforum", Description = "sdfsdfds" });

            var discussion = new Discussion() { Title = title, Id = discussionId, CreatedByUserId = TestUserId, SubforumId = subforumId };
            DiscussionRepository.Instance.Insert(discussion, true);

            var dbDiscussion = DiscussionRepository.Instance.Get(discussionId);

            Assert.IsNotNull(dbDiscussion);
            Assert.True(dbDiscussion.Title.Equals(title));
            Assert.True(dbDiscussion.CreatedByUserId == TestUserId);
            Assert.True(dbDiscussion.SubforumId == subforumId);
        }

        [Test]
        public void AddingDiscussionWithoutSubforumFails()
        {
            var discussion = new Discussion() { Title = "Lala", Id = 1, CreatedByUserId = TestUserId };

#if __MonoCS__
            Assert.Throws<Mono.Data.Sqlite.SqliteException>(() => DiscussionRepository.Instance.Insert(discussion, true));
#else
            Assert.Throws<SQLiteException>(() => DiscussionRepository.Instance.Insert(discussion, true));
#endif
        }

        [Test]
        public void AddingDiscussionWithoutUserFails()
        {
            const long subforumId = 1;

            SubforumRepository.Instance.Insert(new Subforum() { Id = subforumId, Title = "Test subforum", Description = "sdfsdfds" });
            var discussion = new Discussion() { Title = "Lala", Id = 1, SubforumId = subforumId };

#if __MonoCS__
            Assert.Throws<Mono.Data.Sqlite.SqliteException>(() => DiscussionRepository.Instance.Insert(discussion, true));
#else
            Assert.Throws<SQLiteException>(() => DiscussionRepository.Instance.Insert(discussion, true));
#endif
        }

        [Test]
        public void AddingDiscussionWithNonUniqueTitleFails()
        {
            const long subforumId = 1;

            SubforumRepository.Instance.Insert(new Subforum() { Id = subforumId, Title = "Test subforum", Description = "sdfsdfds" });

            var discussion = new Discussion() { Title = "Test discussion", SubforumId = subforumId, CreatedByUserId = TestUserId };
            DiscussionRepository.Instance.Insert(discussion, true);

#if __MonoCS__
            Assert.Throws<Mono.Data.Sqlite.SqliteException>(() => DiscussionRepository.Instance.Insert(discussion, true));
#else
            Assert.Throws<SQLiteException>(() => DiscussionRepository.Instance.Insert(discussion, true));
#endif
        }
    }
}

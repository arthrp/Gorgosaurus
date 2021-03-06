﻿using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.IntegrationTests
{
    [TestFixture]
    public class SubforumTests
    {
        [SetUp]
        public void Setup()
        {
            DbConnector.Delete();
            DbConnector.Init();
        }

        [Test]
        public void CanAddAndReceiveSubforum()
        {
            const string title = "Title";

            SubforumRepository.Instance.Insert(new Subforum()
            {
                Id = 1,
                Title = title,
                Description = "desc 1"
            });

            var subforum = SubforumRepository.Instance.Get(1);
            Assert.True(subforum.Title.Equals(title));
        }

        [Test]
        public void CanUpdateSubforum()
        {
            const string title = "Title";
            const string newTitle = "Title 1";

            var subforum = new Subforum()
            {
                Id = 1,
                Title = title,
                Description = "desc 1"
            };

            SubforumRepository.Instance.Insert(subforum);

            subforum.Title = newTitle;

            SubforumRepository.Instance.Update(subforum);

            var dbSubforum = SubforumRepository.Instance.Get(1);

            Assert.True(dbSubforum.Title.Equals(newTitle));
        }

        [Test]
        public void CanDeleteSubforum()
        {
            const string title = "Title";
            const long id = 1;

            var subforum = new Subforum()
            {
                Id = id,
                Title = title,
                Description = "desc 1"
            };

            SubforumRepository.Instance.Insert(subforum);
            var dbSubforum = SubforumRepository.Instance.Get(id);

            Assert.IsNotNull(dbSubforum);

            SubforumRepository.Instance.Delete(id);
            var newDbSubforum = SubforumRepository.Instance.Get(id);
            Assert.IsNull(newDbSubforum);
        }

        [Test]
        public void AddingDuplicateTitleSubforumFails()
        {
            const string title = "Title";
            var subforum = new Subforum()
            {
                Id = 1,
                Title = title,
                Description = "desc 1"
            };
            SubforumRepository.Instance.Insert(subforum);

            var dbSubforum = SubforumRepository.Instance.Get(1);
            Assert.True(dbSubforum.Title.Equals(title));
            #if __MonoCS__
            Assert.Throws<Mono.Data.Sqlite.SqliteException>(() => SubforumRepository.Instance.Insert(subforum));
            #else
            Assert.Throws<SQLiteException>(() => SubforumRepository.Instance.Insert(subforum));
            #endif
        }

        [Test]
        public void CanGetAllSubforums()
        {
            const string firstTitle = "Title";
            const string secondTitle = "Second title";
            const string firstDesc = "First desc";
            const string secondDesc = "Second desc";

            SubforumRepository.Instance.Insert(new Subforum()
            {
                Title = firstTitle,
                Description = firstDesc
            }, true);

            SubforumRepository.Instance.Insert(new Subforum()
            {
                Title = secondTitle,
                Description = secondDesc
            }, true);

            var subforums = SubforumRepository.Instance.GetAll();

            Assert.True(subforums.Count() == 2);
            Assert.NotNull(subforums.SingleOrDefault(s => s.Title.Equals(firstTitle) && s.Description.Equals(firstDesc)));
            Assert.NotNull(subforums.SingleOrDefault(s => s.Title.Equals(secondTitle) && s.Description.Equals(secondDesc)));
        }


        [TearDown]
        public void TearDown()
        {
            //DbConnector.Delete();
        }
    }
}

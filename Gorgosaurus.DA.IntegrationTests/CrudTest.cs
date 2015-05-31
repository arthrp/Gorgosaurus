using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.IntegrationTests
{
    [TestFixture]
    public class CrudTest
    {
        private const string TEST_DB_NAME = "TestDb";

        [SetUp]
        public void Setup()
        {
            DbConnector.Init(TEST_DB_NAME);
        }

        [Test]
        public void CanAddAndRetrieveEntity()
        {
            DiscussionRepository.Instance.Insert(new Discussion() { Id = 1, Title = "Test discussion" });

            var discussion = DiscussionRepository.Instance.Get(1);
            Assert.True(discussion.Title.Equals("Test discussion"));
        }


        [TearDown]
        public void TearDown()
        {
            DbConnector.Delete();
        }
    }
}

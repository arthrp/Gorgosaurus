using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.IntegrationTests
{
    [TestFixture]
    public class CrudTest
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


        [TearDown]
        public void TearDown()
        {
            DbConnector.Delete();
        }
    }
}

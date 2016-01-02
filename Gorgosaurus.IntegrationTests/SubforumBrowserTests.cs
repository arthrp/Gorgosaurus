using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA;
using Gorgosaurus.DA.Repositories;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.IntegrationTests
{
    [TestFixture]
    public class SubforumBrowserTests
    {
        [SetUp]
        public void Setup()
        {
            DbConnector.Delete();
            DbConnector.Init();
        }

        [Test]
        public void CanGetSubforum()
        {
            SubforumRepository.Instance.Insert(new Subforum()
            {
                Id = 1,
                Title = "Test",
                Description = "talking about important stuff"
            });

            var bootstrapper = new GorgosaurusBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            var result = browser.Get("/subforum/Test", with => { with.HttpRequest(); });

            Assert.True(result.StatusCode == HttpStatusCode.OK);
        }
    }
}

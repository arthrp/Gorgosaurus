using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA;
using Gorgosaurus.DA.Repositories;
using Gorgosaurus.Models;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.IntegrationTests
{
    [TestFixture]
    public class ForumBrowserTests
    {
        [SetUp]
        public void SetUp()
        {
            DbConnector.Delete();
            DbConnector.Init();
        }

        [Test]
        public void CanGetAllSubforums()
        {
            const string firstBrowserTitle = "Test first";
            const string secondBrowserTitle = "Test second";

            SubforumRepository.Instance.Insert(new Subforum()
            {
                Id = 1,
                Title = firstBrowserTitle,
                Description = "talking about important stuff"
            });

            SubforumRepository.Instance.Insert(new Subforum()
            {
                Id = 2,
                Title = secondBrowserTitle,
                Description = "talking about important stuff"
            });

            var bootstrapper = new GorgosaurusBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            var result = browser.Get("/subforums");

            Assert.True(result.StatusCode == Nancy.HttpStatusCode.OK);
            var resultStr = result.Body.AsString();

            var subforums = JsonConvert.DeserializeObject<ForumModel>(resultStr);

            Assert.True(subforums.Subforums.Count() == 2);
        }
    }
}

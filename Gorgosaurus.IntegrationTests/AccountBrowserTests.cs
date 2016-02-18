using Gorgosaurus.BO.Entities;
using Gorgosaurus.Common;
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
    public class AccountBrowserTests : BaseAccountTests
    {
        private const string USERNAME = "testuser";
        private const string PASSWORD = "test";

        [SetUp]
        public void Setup()
        {
            DbConnector.Delete();
            DbConnector.Init();

            CreateUser(USERNAME, PASSWORD);
        }

        [Test]
        public void CanLoginAndGetCurrentUser()
        {
            var bootstrapper = new GorgosaurusBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            var resp = browser.Post("/account/login", with =>
            {
                with.HttpRequest();
                with.JsonBody<LoginModel>( new LoginModel() { Username = USERNAME, Password = PASSWORD} );
            });

            Assert.True(resp.StatusCode == Nancy.HttpStatusCode.OK);
            Assert.Greater(resp.Cookies.Count(), 0);

            var authCookie = resp.Cookies.SingleOrDefault(c => c.Name == Constants.AUTH_COOKIE_NAME);
            Assert.NotNull(authCookie);
            Assert.True(!String.IsNullOrEmpty(authCookie.Value));

            var userResp = browser.Get("/account/current", with => with.HttpRequest());

            Assert.True(userResp.StatusCode == Nancy.HttpStatusCode.OK);
            var returnedUsername = JsonConvert.DeserializeObject<BasicUserInfo>(userResp.Body.AsString());
            Assert.True(returnedUsername.Username.Equals(USERNAME));
        }
    }
}

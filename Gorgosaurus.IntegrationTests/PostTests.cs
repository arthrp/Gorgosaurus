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
    public class PostTests : AccountBrowserTests
    {
        private const string USERNAME = "testuser";
        private const string PASSWORD = "test";
        private const bool IS_ADMIN = true;
        private const string NAME = "Tester";
        private const string SURNAME = "Testerson";
        private const long FIRST_USER_ID = 1;

        private const string _firstSubforumName = "Test first";
        private const long FIRST_SUBFORUM_ID = 1;

        [SetUp]
        public void SetUp()
        {
            DbConnector.Delete();
            DbConnector.Init();

            CreateUser(USERNAME, PASSWORD, IS_ADMIN, NAME, SURNAME, id: FIRST_USER_ID);

            SubforumRepository.Instance.Insert(new Subforum()
            {
                Id = FIRST_SUBFORUM_ID,
                Title = _firstSubforumName,
                Description = "talking about important stuff",
                CreatedByUserId = FIRST_USER_ID
            });
        }

        [Test]
        public void CanPostAsAnonymousUser()
        {
            const string discussionTitle = "Hello";
            const long discussionId = 1;
            const string postText = "Hello, post";

            DiscussionRepository.Instance.Insert(new Discussion()
            {
                Id = discussionId,
                Title = discussionTitle,
                SubforumId = 1,
                CreatedByUserId = FIRST_USER_ID
            });

            var bootstrapper = new GorgosaurusBootstrapper();
            var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

            var postAddResponse = browser.Post("/post/add", with =>
            {
                with.HttpRequest();
                with.JsonBody<ForumPost>(new ForumPost() { PostText = postText, DiscussionId = discussionId });
            });

            Assert.True(postAddResponse.StatusCode == HttpStatusCode.OK);

            var disc = DiscussionRepository.Instance.Get(discussionId);
            Assert.True(disc.Posts.Count() == 1);

            var post = disc.Posts.FirstOrDefault();
            Assert.True(post.PostText == postText);
            Assert.True(post.DiscussionId == discussionId);
        }
    }
}

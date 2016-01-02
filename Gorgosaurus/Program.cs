namespace Gorgosaurus
{
    using System;
    using Nancy.Hosting.Self;
    using Gorgosaurus.DA;
    using Gorgosaurus.DA.Repositories;
    using Gorgosaurus.BO.Entities;
    using System.Diagnostics;
    using Gorgosaurus.BO.Extensions;
    using Common;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:8080");

            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            using (var host = new NancyHost(uri, new GorgosaurusBootstrapper(), hostConfigs))
            {
                host.Start();

                DbConnector.Delete();

                DbConnector.Init();

                string adminSalt = "xyz00";
                string adminPass = "haha";

                string hash = CryptoHelper.GenerateHash(adminPass, adminSalt);

                SubforumRepository.Instance.Insert(new Subforum()
                {
                    Id = 1,
                    Title = "Main forum",
                    Description = "talking about important stuff"
                });

                UserRepository.Instance.Insert(new ForumUser()
                {
                    Id = 1,
                    Username = "admin",
                    Password = adminSalt + hash,
                    IsAdmin = true,
                    Name = "Vladimir",
                    Surname = "Johnson"
                });

                DiscussionRepository.Instance.Insert(new Discussion() {
                    Id = 1,
                    Title = "Hello!",
                    SubforumId = 1,
                    CreatedOnUnix = DateTime.UtcNow.ToUnixTimestamp(),
                    CreatedByUserId = 1 });
                DiscussionRepository.Instance.Insert(new Discussion()
                {
                    Id = 2,
                    Title = "Fuck",
                    SubforumId = 1,
                    CreatedOnUnix = DateTime.UtcNow.ToUnixTimestamp(),
                    CreatedByUserId = 1
                });
                var post = new ForumPost() { Id = 1, PostText = "crap", DiscussionId = 1, CreatedByUserId = 1 };
                ForumPostRepository.Instance.Insert(post);
                var ent = ForumPostRepository.Instance.Get(1);

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");

                ent.PostText = "Really nice post";
                ForumPostRepository.Instance.Update(ent);

                var entNew = ForumPostRepository.Instance.Get(1);

                SubforumRepository.Instance.Insert(new Subforum()
                {
                    Title = "New",
                    Description = "talking about important stuff"
                });


                var user = UserRepository.Instance.Get(1);

               Console.ReadLine();
            }
        }
    }
}

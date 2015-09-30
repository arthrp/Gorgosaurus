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
            var uri =
                new Uri("http://localhost:8080");

            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            using (var host = new NancyHost(uri, new Bootstrapper(), hostConfigs))
            {
                host.Start();

                DbConnector.Delete();

                DbConnector.Init();

                string adminSalt = "xyz00";
                string adminPass = "haha";

                string hash = CryptoHelper.GenerateHash(adminPass, adminSalt);
                File.WriteAllText(@"F:\Temp\pass.txt", hash);

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
                    Password = adminSalt +
                        @"Quc4nirwMQgcDz+iSvKg6RPqPjbVJf6hiocz1V0WYqSRWDdXpLamVOQT0Bx1Rzk7sGOSY0ar6bKNH1xLkwP7ulDgSw34LOtIkNOuyDljJ3wgJtPh7Zov8v0oQnDAofs5IKbfw2AH8KiFYeysgYj1BO5gzdoLk568wJwtRs6vefUUDF+9qH9lv86mZnMFVwxzGZ8o/0HamZM126wxALc35UbhyQbZmgRLg6Np7WlbniB4dbAPgJq8tgvQeYyWolu83VUYI8yupxLeNuwNj5qxUXHhzG8bYZNnb4pBSbbwwuPFHeG6PfAf2jiFJUv/ePo3OFqGrsgGlPtqR2DvyaRs6A==",
                    IsUserAdmin = true
                });

                DiscussionRepository.Instance.Insert(new Discussion() { 
                    Id = 1, Title = "Hello!", SubforumId = 1, CreatedOnUnix = DateTime.UtcNow.ToUnixTimestamp(), CreatedByUserId = 1 });
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
                    Title = null,
                    Description = "talking about important stuff"
                });
               

                var user = UserRepository.Instance.Get(1);

               Console.ReadLine();
            }
        }
    }
}

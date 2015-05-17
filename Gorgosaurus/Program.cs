namespace Gorgosaurus
{
    using System;
    using Nancy.Hosting.Self;
    using Gorgosaurus.DA;
    using Gorgosaurus.DA.Repositories;
    using Gorgosaurus.BO.Entities;

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

                DbConnector.Init();

                DiscussionRepository.Instance.Insert(new Discussion() { Id = 1, Title = "Hello!" });
                ForumPostRepository.Instance.Insert(new ForumPost() { Id = 1, PostText = "crap", DiscussionId = 1 });
                var ent = ForumPostRepository.Instance.Get(1);

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}

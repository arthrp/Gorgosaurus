namespace Gorgosaurus
{
    using System;
    using Nancy.Hosting.Self;
    using Gorgosaurus.DA;

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

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}

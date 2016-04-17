namespace Gorgosaurus
{
    using Modules;
    using Nancy;

    public class IndexModule : BaseModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };
        }
    }
}
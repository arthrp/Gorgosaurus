using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using Nancy;
using Nancy.Helpers;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Modules
{
    public class SubforumModule : NancyModule
    {
        public SubforumModule()
        {
            Get["/subforum/{title}"] = parameters =>
            {
                
                string subforumTitle = HttpUtility.UrlDecode(parameters.title);

                if (String.IsNullOrEmpty(subforumTitle))
                    return HttpStatusCode.BadRequest;

                int? page = this.Request.Query["page"];

                var subforum = SubforumRepository.Instance.Get(subforumTitle, page);

                return Response.AsJson<Subforum>(subforum);
            };
        }
    }
}

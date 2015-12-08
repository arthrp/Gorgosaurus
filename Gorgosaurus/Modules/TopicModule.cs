using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gorgosaurus.Modules
{
    public class TopicModule : BaseModule
    {
        public TopicModule()
        {
            Get["/discussion/{id:int}"] = parameters => 
            {
                int id = parameters.id;
                var discussion = DiscussionRepository.Instance.Get(id);
                return Response.AsJson<Discussion>(discussion);
            };

            Post["/discussion/add"] = parameters =>
            {
                bool isAuthenticated = IsAuthenticated();
                if (!isAuthenticated)
                    return HttpStatusCode.Forbidden;

                var newDiscussion = this.Bind<Discussion>();

                DiscussionRepository.Instance.Insert(newDiscussion, true);

                return HttpStatusCode.OK;
            };
        }
    }
}

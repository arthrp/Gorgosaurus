using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using Nancy;
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
        }
    }
}

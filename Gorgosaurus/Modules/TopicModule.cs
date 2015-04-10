using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gorgosaurus
{
    public class TopicModule : NancyModule
    {
        public TopicModule()
        {
            Get["/topic/{id:int}"] = parameters => 
            {
                int id = parameters.id;
                var resPost = ForumPostRepository.Instance.Get(id);
                return Response.AsJson<ForumPost>(resPost);
            };
        }
    }
}

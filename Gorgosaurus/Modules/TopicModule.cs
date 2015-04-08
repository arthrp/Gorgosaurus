using Gorgosaurus.BO.Entities;
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
                var resPost = new ForumPost() { PostText = "Hello,"+parameters.id };
                return Response.AsJson<ForumPost>(resPost);
            };
        }
    }
}

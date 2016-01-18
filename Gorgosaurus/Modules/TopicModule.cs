using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using Gorgosaurus.Models;
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
                var user = GetCurrentUser();

                if(user == null)
                    return HttpStatusCode.Forbidden;

                var discussionPresentationModel = this.Bind<DiscussionPresentationModel>();
                var newDiscussion = this.Bind<Discussion>();
                newDiscussion.CreatedByUserId = user.Id;
                
                DiscussionRepository.Instance.Insert(newDiscussion, discussionPresentationModel.FirstPostText);

                return HttpStatusCode.OK;
            };
        }
    }
}

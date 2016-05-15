using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;
using Gorgosaurus.Models;

namespace Gorgosaurus.Modules
{
    public class PostModule : BaseModule
    {
        public PostModule()
        {
            Post["/post/add"] = parameters =>
            {
                var newForumPost = this.Bind<ForumPost>();

                if (!ValidatePostModel(newForumPost))
                    return HttpStatusCode.BadRequest;

                var user = GetCurrentUser();

                if (user != null)
                    newForumPost.CreatedByUserId = user.Id;

                ForumPostRepository.Instance.Insert(newForumPost, true);

                return HttpStatusCode.OK;
            };

            Put["/post/update"] = paramters =>
            {
                if (!IsAuthenticated() || IsUserAdmin())
                    return HttpStatusCode.Forbidden;

                var updatedPost = this.Bind<ForumPost>();

                if (!ValidatePostModel(updatedPost))
                    return HttpStatusCode.BadRequest;

                ForumPostRepository.Instance.Update(updatedPost);

                return HttpStatusCode.OK;
            };

            Delete["/post/remove/{id:int}"] = parameters =>
            {
                if (!IsAuthenticated() || IsUserAdmin())
                    return HttpStatusCode.Forbidden;

                ForumPostRepository.Instance.Delete(parameters.id);

                return HttpStatusCode.OK;
            };
        }

        //Unfortunately AOP is out of question with methods that Nancy provides :)
        private bool ValidatePostModel(ForumPost forumPost)
        {
            bool isValidRequest = !String.IsNullOrEmpty(forumPost.PostText);

            return isValidRequest;
        }
    }
}

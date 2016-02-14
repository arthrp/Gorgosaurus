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

                var user = GetCurrentUser();

                if (user != null)
                    newForumPost.CreatedByUserId = user.Id;

                ForumPostRepository.Instance.Insert(newForumPost, true);

                return HttpStatusCode.OK;
            };

            Put["/post/update"] = paramters =>
            {
                var updatedPost = this.Bind<ForumPost>();

                ForumPostRepository.Instance.Update(updatedPost);

                return HttpStatusCode.OK;
            };

            Delete["/post/remove/{id:int}"] = parameters =>
            {
                var user = GetCurrentUser();
                if (user == null || !user.IsAdmin)
                    return HttpStatusCode.Forbidden;

                ForumPostRepository.Instance.Delete(parameters.id);

                return HttpStatusCode.OK;
            };
        }

    }
}

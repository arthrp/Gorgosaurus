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
    public class PostModule : NancyModule
    {
        public PostModule()
        {
            Post["/post/add"] = parameters =>
            {
                var newForumPost = this.Bind<ForumPost>();

                var requestCookie = Request.Cookies.FirstOrDefault(c => c.Key == AccountModule.AUTH_COOKIE_NAME);
                var sessionId = requestCookie.Value;

                var user = SimpleSession.Instance.Get<ForumUser>(sessionId);

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
                ForumPostRepository.Instance.Delete(parameters.id);

                return HttpStatusCode.OK;
            };
        }

    }
}

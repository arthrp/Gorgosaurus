using Gorgosaurus.BO.Entities;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Modules
{
    public abstract class BaseModule : NancyModule
    {
        protected bool IsAuthenticated()
        {
            var user = GetCurrentUser();

            return user != null;
        }

        protected ForumUser GetCurrentUser()
        {
            var requestCookie = Request.Cookies.FirstOrDefault(c => c.Key == Constants.AUTH_COOKIE_NAME);
            var sessionId = requestCookie.Value;

            var user = SimpleSession.Instance.Get<ForumUser>(sessionId);

            return user;
        }
    }
}

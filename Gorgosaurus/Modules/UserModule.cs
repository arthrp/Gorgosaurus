using Gorgosaurus.DA.Repositories;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Modules
{
    public class UserModule : BaseModule
    {
        public UserModule()
        {
            Get["/user/{username}"] = parameters =>
            {
                if (!IsAuthenticated())
                    return HttpStatusCode.BadRequest;

                string username = parameters.username;

                var user = UserRepository.Instance.Get(username);

                if (user == null)
                    return HttpStatusCode.NotFound;

                user.Password = "";

                return user;
            };
        }
    }
}

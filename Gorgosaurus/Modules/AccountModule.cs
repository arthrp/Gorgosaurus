using Gorgosaurus.Models;
using Nancy;
using Nancy.Cookies;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Modules
{
    public class AccountModule : NancyModule
    {
        private const string AUTH_COOKIE_NAME = "Grg_user";

        public AccountModule()
        {
            Post["account/login"] = parameters =>
            {
                var loginModel = this.Bind<LoginModel>();

                var response = new Response() { StatusCode = HttpStatusCode.OK };
                response.Cookies.Add(GenerateCookie(loginModel.Username, DateTime.Now.AddMonths(3)));

                return response;
            };

            Post["account/logout"] = parameters =>
            {
                var response = new Response() { StatusCode = HttpStatusCode.OK };

                var requestCookie = Request.Cookies.FirstOrDefault(c => c.Key == AUTH_COOKIE_NAME);
                string userName = requestCookie.Value;

                if (String.IsNullOrEmpty(userName))
                    return HttpStatusCode.OK;

                var cookieToAdd = GenerateCookie(userName, DateTime.Now.AddYears(-1));
                response.Cookies.Add(cookieToAdd);

                return response;
            };

            Get["account/current"] = parameters =>
            {
                var requestCookie = Request.Cookies.FirstOrDefault(c => c.Key == AUTH_COOKIE_NAME);
                if (String.IsNullOrEmpty(requestCookie.Value))
                    return HttpStatusCode.OK;

                return requestCookie.Value;
            };
        }

        private NancyCookie GenerateCookie(string userName, DateTime expiresAt)
        {
            return new NancyCookie(name: AUTH_COOKIE_NAME, value: userName, httpOnly: true) { Path = "/", Expires = expiresAt };
        }
    }
}

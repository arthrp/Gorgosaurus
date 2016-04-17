using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Managers;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Modules
{
    public class AdminModule : BaseModule
    {
        public AdminModule()
        {
            Get["/settings"] = parameters =>
            {
                var user = GetCurrentUser();

                if (user == null || user.IsAdmin == false)
                    return HttpStatusCode.Forbidden;

                var settings = GlobalSettingsManager.Instance.LoadAll();

                return Response.AsJson<List<GlobalSetting>>(settings);
            };

            Post["/settings"] = parameters =>
            {
                var user = GetCurrentUser();

                if (user == null || user.IsAdmin == false)
                    return HttpStatusCode.Forbidden;

                var settings = this.Bind<List<GlobalSetting>>();

                GlobalSettingsManager.Instance.SaveAll(settings);

                return HttpStatusCode.OK;
            };
        }
    }
}

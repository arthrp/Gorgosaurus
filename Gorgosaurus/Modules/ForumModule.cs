using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Managers;
using Gorgosaurus.DA.Repositories;
using Gorgosaurus.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Modules
{
    public class ForumModule : BaseModule
    {
        public ForumModule()
        {
            Get["/subforums"] = parameters =>
            {
                var subforums = SubforumRepository.Instance.GetAll();

                var forumName = GlobalSettingsManager.Instance.Load(DA.GlobalSettingsEnum.ForumName);

                var fm = new ForumModel() { Subforums = subforums, ForumTitle = forumName };

                return Response.AsJson<ForumModel>(fm);
            };
        }
    }
}

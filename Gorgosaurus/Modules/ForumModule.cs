using Gorgosaurus.DA.Repositories;
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

                return subforums;
            };
        }
    }
}

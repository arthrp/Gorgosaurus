using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Models
{
    public class ForumModel
    {
        public string ForumTitle { get; set; }

        public IEnumerable<Subforum> Subforums { get; set; }
    }
}

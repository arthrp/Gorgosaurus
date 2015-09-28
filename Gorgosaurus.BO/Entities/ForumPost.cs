using Gorgosaurus.BO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Entities
{
    public class ForumPost : BaseEntity
    {
        public string PostText { get; set; }

        public long? DiscussionId { get; set; }

        [NotColumn]
        public string CreatedByUsername { get; set; }
    }
}

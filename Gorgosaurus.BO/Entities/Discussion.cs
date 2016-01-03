using Gorgosaurus.BO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Entities
{
    public class Discussion : BaseEntity
    {
        public string Title { get; set; }

        public long SubforumId { get; set; }

        public IEnumerable<ForumPost> Posts { get; set; }

        [NotColumn]
        public string CreatedByUsername { get; set; }
    }
}

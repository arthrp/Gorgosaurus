using Gorgosaurus.BO.Attributes;
using Gorgosaurus.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Entities
{
    public class Subforum : BaseEntity, IHasPagedCollection
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<Discussion> Discussions { get; set; }

        [NotColumn]
        public int TotalPages { get; set; }
    }
}

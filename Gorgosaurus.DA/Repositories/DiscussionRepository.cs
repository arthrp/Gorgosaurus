using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Repositories
{
    public class DiscussionRepository : BaseRepository<Discussion>
    {
        public static readonly DiscussionRepository Instance = new DiscussionRepository();
    }
}

using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Repositories
{
    public class ForumPostRepository : BaseRepository<ForumPost>
    {
        public static ForumPostRepository Instance = new ForumPostRepository();


    }
}

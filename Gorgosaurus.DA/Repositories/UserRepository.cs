using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Repositories
{
    public class UserRepository : BaseRepository<ForumUser>
    {
        public static UserRepository Instance = new UserRepository();
    }
}

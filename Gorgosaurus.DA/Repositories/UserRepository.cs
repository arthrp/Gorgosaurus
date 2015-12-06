using AutoMapper;
using Dapper;
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

        public ForumUser Get(string username)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var user = conn.Query<ForumUser>("select * from ForumUser where Username = :username limit 1", new { username }).FirstOrDefault();

                return user;
            }
        }
    }
}

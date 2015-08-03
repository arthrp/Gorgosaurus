using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Gorgosaurus.DA.Repositories
{
    public class DiscussionRepository : BaseRepository<Discussion>
    {
        public static readonly DiscussionRepository Instance = new DiscussionRepository();

        public override Discussion Get(long id)
        {
            var discussion =  base.Get(id);

            string sql = String.Format("select * from {0} where DiscussionId={1}", typeof(ForumPost).Name, id);

            using (var conn = DbConnector.GetOpenConnection())
            {
                var posts = conn.Query<ForumPost>(sql);

                discussion.Posts = posts;
            }

            return discussion;
        }
    }
}

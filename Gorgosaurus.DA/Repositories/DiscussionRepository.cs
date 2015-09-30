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

            var props = new ForumPost().GetPropertiesAsCsv();

            string sql = String.Format(
                @"select {0},fu.Username as CreatedByUsername from {1} left outer join {2} as fu on {1}.CreatedByUserId = fu.Id where DiscussionId = :discussionId", props, 
                    typeof(ForumPost).Name, typeof(ForumUser).Name);

            using (var conn = DbConnector.GetOpenConnection())
            {
                var posts = conn.Query<ForumPost>(sql, new { discussionId = id });

                discussion.Posts = posts;
            }

            return discussion;
        }
    }
}

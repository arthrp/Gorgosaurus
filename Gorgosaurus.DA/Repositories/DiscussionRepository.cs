using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gorgosaurus.BO.Extensions;

namespace Gorgosaurus.DA.Repositories
{
    public class DiscussionRepository : BaseRepository<Discussion>
    {
        public static readonly DiscussionRepository Instance = new DiscussionRepository();

        public override Discussion Get(long id)
        {
            var discussion = base.Get(id);

            if (discussion == null)
                return null;

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

        public void Insert(Discussion discussion, string firstPostText)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                discussion.CreatedOnUnix = DateTime.UtcNow.ToUnixTimestamp();
                var sql = GetInsertSql(discussion, true);

                var x = conn.Execute(sql);

                long id = conn.ExecuteScalar<long>(String.Format("select id from {0} where Title = :title", typeof(Discussion).Name), new { title = discussion.Title });

                var forumPost = new ForumPost() { CreatedOnUnix = discussion.CreatedOnUnix, DiscussionId = id, PostText = firstPostText, CreatedByUserId = discussion.CreatedByUserId };
                ForumPostRepository.Instance.Insert(forumPost, true);
            }
        }
    }
}

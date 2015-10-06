using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Gorgosaurus.DA.Repositories
{
    public class SubforumRepository : BaseRepository<Subforum>
    {
        public static readonly SubforumRepository Instance = new SubforumRepository();

        public override Subforum Get(long id)
        {
            var res = base.Get(id);

            string sql = String.Format("select * from {0} where SubforumId = :id", typeof(Discussion).Name);

            using (var conn = DbConnector.GetOpenConnection())
            {
                var discussions = conn.Query<Discussion>(sql, new { id });

                res.Discussions = discussions;
            }

            return res;
        }

        public Subforum Get(string title)
        {
            string sql = String.Format(@"select * from {0} where Title = :title", typeof(Subforum).Name);
            string discussionsSql = String.Format("select * from {0} where SubforumId = :id", typeof(Discussion).Name);

            Subforum res = null;

            using (var conn = DbConnector.GetOpenConnection())
            {
                res = conn.Query<Subforum>(sql, new { title }).FirstOrDefault();

                if(res != null)
                {
                    var discussions = conn.Query<Discussion>(discussionsSql, new { id = res.Id });

                    res.Discussions = discussions;
                }
            }

            return res;
        }
    }
}

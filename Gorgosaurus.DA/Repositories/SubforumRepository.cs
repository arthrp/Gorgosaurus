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

            string sql = String.Format("select * from {0} where SubforumId={1}", typeof(Discussion).Name, id);

            using (var conn = DbConnector.GetOpenConnection())
            {
                var discussions = conn.Query<Discussion>(sql);

                res.Discussions = discussions;
            }

            return res;
        }
    }
}

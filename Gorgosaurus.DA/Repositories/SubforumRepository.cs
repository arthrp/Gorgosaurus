using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gorgosaurus.DA.Managers;

namespace Gorgosaurus.DA.Repositories
{
    public class SubforumRepository : BaseRepository<Subforum>
    {
        public static readonly SubforumRepository Instance = new SubforumRepository();

        public override Subforum Get(long id)
        {
            var res = base.Get(id);

            if (res == null)
                return null;

            string sql = String.Format("select * from {0} where SubforumId = :id", typeof(Discussion).Name);

            using (var conn = DbConnector.GetOpenConnection())
            {
                var discussions = conn.Query<Discussion>(sql, new { id });

                res.Discussions = discussions;
            }

            return res;
        }

        public Subforum Get(string title, int? page)
        {
            int pageSize = Int32.Parse(GlobalSettingsManager.Instance.Load(GlobalSettingsEnum.PageSize));
            int skipRecordsCount = (page != null) ? page.Value * pageSize : 0;
            string sql = String.Format(@"select * from {0} where Title = :title", typeof(Subforum).Name);
            string discussionProps = new Discussion().GetPropertiesAsCsv();
            string discussionsSql = String.Format(
                @"select {0}, fu.Username as CreatedByUsername from {1} left outer join {2} as fu on {1}.CreatedByUserId = fu.Id 
                where {1}.SubforumId = :id
                and {1}.Id not in (select id from {1} where SubforumId = :id order by CreatedOnUnix desc limit :skipRecordsCount)
                order by {1}.CreatedOnUnix desc limit :pageSize", discussionProps,
                typeof(Discussion).Name, typeof(ForumUser).Name);

            Subforum res = null;

            using (var conn = DbConnector.GetOpenConnection())
            {
                res = conn.Query<Subforum>(sql, new { title }).FirstOrDefault();

                if (res != null)
                {
                    var discussions = conn.Query<Discussion>(discussionsSql, new { id = res.Id, skipRecordsCount, pageSize });

                    res.Discussions = discussions;

                    var totalRecords = conn.ExecuteScalar<int>(String.Format("select count(*) from {0} where SubforumId = :subforumId", typeof(Discussion).Name), 
                        new { subforumId = res.Id });

                    res.TotalPages = (int)Math.Round((double)(totalRecords / pageSize), MidpointRounding.ToEven);
                }
            }

            return res;
        }

        public IEnumerable<Subforum> GetAll()
        {
            string sql = String.Format(@"select * from {0}", typeof(Subforum).Name);

            IEnumerable<Subforum> res;

            using (var conn = DbConnector.GetOpenConnection())
            {
                res = conn.Query<Subforum>(sql);
            }

            return res;
        }
    }
}

using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Gorgosaurus.DA.Extensions;
using Dapper;
using System.Diagnostics;
using Gorgosaurus.BO.Extensions;

namespace Gorgosaurus.DA.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly string _entityName = typeof(T).Name;

        public virtual T Get(long id)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var results = conn.Query<T>(String.Format("select * from {0} where Id = :id", _entityName), new { id });

                return results.FirstOrDefault();
            }
        }

        public virtual void Insert(T obj, bool skipId = false)
        {
            obj.CreatedOnUnix = DateTime.UtcNow.ToUnixTimestamp();

            using (var conn = DbConnector.GetOpenConnection())
            {
                var sqlFirstPart = new StringBuilder("insert into " + _entityName + "(");
                var sqlSecondPart = new StringBuilder(" values(");

                PropertyInfo[] properties = obj.GetType().GetProperties();
                string propValue = "";
                foreach (PropertyInfo property in properties)
                {
                    if (!property.CanWrite ||
                        property.IsEnumerable() ||
                        (property.IsCurrentEntityId() && skipId) ||
                        property.CustomAttributes.Any(a => a.AttributeType.Name == "NotColumn"))
                        continue;

                    var attrs = property.CustomAttributes;

                    propValue = (property.IsNumeric()) ?
                        property.GetValue(obj).ToString() : "'" + property.GetValue(obj) + "'";

                    sqlFirstPart.Append(property.Name.ToLower() + ",");
                    sqlSecondPart.Append(propValue + ",");
                }

                //deleting last commas
                sqlFirstPart.Length -= 1;
                sqlSecondPart.Length -= 1;

                sqlFirstPart.Append(")");
                sqlSecondPart.Append(")");

                int affected = conn.Execute(sqlFirstPart.ToString() + sqlSecondPart.ToString());

                //Debug.WriteLine("inserting " + affected + " row(s)");
            }
        }

        public virtual void Update(T obj)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var sqlFirstPart = new StringBuilder("update " + _entityName);
                var sqlSecondPart = new StringBuilder(" set ");
                string sqlThirdPart = " where Id=" + obj.Id;

                PropertyInfo[] properties = obj.GetType().GetProperties();
                string propValue = "";
                foreach (PropertyInfo property in properties)
                {
                    if (!property.CanWrite ||
                        property.IsEnumerable() ||

                        property.CustomAttributes.Any(a => a.AttributeType.Name == "NotColumn"))
                        continue;

                    propValue = (property.IsNumeric()) ?
                        property.GetValue(obj).ToString() : "'" + property.GetValue(obj) + "'";

                    sqlSecondPart.Append(property.Name.ToLower() + "=" + propValue + ",");

                }

                sqlSecondPart.Length -= 1;

                int affected = conn.Execute(sqlFirstPart.ToString() + sqlSecondPart.ToString() + sqlThirdPart.ToString());

                Debug.WriteLine("updating " + affected + " row(s)");
            }
        }

        public virtual void Delete(long id)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                string sql = String.Format("delete from {0} where id={1}", _entityName, id);

                int affected = conn.Execute(sql);

                Debug.WriteLine("deleted " + affected + " row(s)");
            }
        }
    }
}

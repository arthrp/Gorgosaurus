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

namespace Gorgosaurus.DA.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly string _entityName = typeof(T).Name;

        public T Get(long id)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var results = conn.Query<T>(String.Format("select * from {0} where Id = :id", _entityName), new { id });

                return results.FirstOrDefault();
            }
        }

        public void Insert(T obj)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var sqlFirstPart = new StringBuilder("insert into " + _entityName + "(");
                var sqlSecondPart = new StringBuilder(" values(");

                PropertyInfo[] properties = obj.GetType().GetProperties();
                string propValue = "";
                foreach (var property in properties)
                {
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

                //var cmd = new SQLiteCommand(sqlFirstPart.ToString()+sqlSecondPart.ToString(), conn);
                int affected = conn.Execute(sqlFirstPart.ToString()+sqlSecondPart.ToString());

                Debug.WriteLine(affected);
            }
        }
    }
}

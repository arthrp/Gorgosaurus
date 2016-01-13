using Dapper;
using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA
{
    public class DbCreator
    {
        public static void CreateDbStructure()
        {
            using (var conn = DbConnector.GetRawConnection())
            {
                conn.Open();

                string outputDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                string sql = File.ReadAllText(outputDir + "//Create.sql");


                var cmd = new SqliteCommandWrapper(sql, conn);
                cmd.GetInner().ExecuteNonQuery();

                conn.Close();
            }
        }

        public static void EnableForeignKeys()
        {
            using (var conn = DbConnector.GetRawConnection())
            {
                conn.Open();

                var cmd = new SqliteCommandWrapper("PRAGMA foreign_keys = ON", conn);

                cmd.GetInner().ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void InsertDefaultSettings()
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var settings = SettingsManager.Instance.GetDefaultSettings();
                foreach(var setting in settings)
                {
                    conn.Execute("insert into GlobalSettings(Name, Value) values(:name, :value)", new { name = setting.Key, value = setting.Value });
                }
            }
        }
    }
}

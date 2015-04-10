using Gorgosaurus.BO.Entities;
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
        public static void Create()
        {
            using (var conn = DbConnector.GetRawConnection())
            {
                conn.Open();

                string outputDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string sql = File.ReadAllText(outputDir + "//Create.sql");

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}

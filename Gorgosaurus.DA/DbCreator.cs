using System;
using System.Collections.Generic;
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
            using (var conn = DbConnector.GetConnection())
            {
                conn.Open();

                string outputDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string sql = File.ReadAllText(outputDir + "//Create.sql");

                

                conn.Close();
            }
        }
    }
}

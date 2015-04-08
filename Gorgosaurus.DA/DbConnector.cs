using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA
{
    public class DbConnector
    {
        public const string DB_NAME = "MainDb";

        public static string DbFile
        {
            get 
            {
                Debug.WriteLine(Environment.CurrentDirectory);
                return Environment.CurrentDirectory + "\\" + DB_NAME + ".sqlite"; 
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public static void Init()
        {
            if (!File.Exists(DbFile))
            {
                SQLiteConnection.CreateFile(DbFile);
            }

            DbCreator.Create();
        }
    }
}

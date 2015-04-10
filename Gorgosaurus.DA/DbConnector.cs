using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

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

        public static SQLiteConnection GetRawConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public static IDbConnection GetOpenConnection()
        {
            var conn = new SQLiteConnection("Data Source=" + DbFile);
            return conn.OpenAndReturn();
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

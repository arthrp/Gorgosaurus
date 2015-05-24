using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
#if __MonoCS__
using Mono.Data.Sqlite;
#else
using System.Data.SQLite;
#endif

namespace Gorgosaurus.DA
{
    public class DbConnector
    {
        public const string DEFAULT_DB_NAME = "MainDb";

        private static string _dbName = DEFAULT_DB_NAME;

        public static string DbFile
        {
            get 
            {
                Debug.WriteLine(Environment.CurrentDirectory);
                return Environment.CurrentDirectory + "\\" + _dbName + ".sqlite";
            }
        }

        #if __MonoCS__
        public static SqliteConnection GetRawConnection()
        #else
        public static SQLiteConnection GetRawConnection()
        #endif
        {
            return new SqlLiteConnectionWrapper("Data Source=" + DbFile).GetInner();
        }

        public static IDbConnection GetOpenConnection()
        {
            var conn = new SqlLiteConnectionWrapper("Data Source=" + DbFile).GetInner();
            conn.Open();

            return conn;
        }

        public static void Init(string dbNameOverride = null)
        {
            if (!String.IsNullOrEmpty(dbNameOverride))
            {
                _dbName = dbNameOverride;
            }

            if (!File.Exists(DbFile))
            {
                SqlLiteConnectionWrapper.CreateFile(DbFile);
            }

            DbCreator.CreateDbStructure();
        }

        public static void Delete()
        {
            if (File.Exists(DbFile))
            {
                File.Delete(DbFile);
            }
        }
    }
}

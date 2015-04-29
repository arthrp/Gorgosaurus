using System;
#if __MonoCS__
using Mono.Data.Sqlite;
#else
using System.Data.SQLite;
#endif

namespace Gorgosaurus.DA
{
    /// <summary>
    /// Wrapping around .NET and Mono SqlLite libs for unified API
    /// </summary>
    public class SqlLiteConnectionWrapper
    {
        #if __MonoCS__
        private readonly SqliteConnection _connection;
        #else
        private readonly SQLiteConnection _connection;
        #endif

        public SqlLiteConnectionWrapper(string dataSource)
        {
            #if __MonoCS__
            _connection = new SqliteConnection(dataSource);
            #else
            _connection = new SQLiteConnection(dataSource);
            #endif
        }

        public static void CreateFile(string dbFileName)
        {
            #if __MonoCS__
            SqliteConnection.CreateFile(dbFileName);
            #else
            SQLiteConnection.CreateFile(dbFileName);
            #endif
        }

        #if __MonoCS__
        public SqliteConnection GetInner()
        #else
        public SQLiteConnection GetInner()
        #endif
        {
            return _connection;
        }

    }
}


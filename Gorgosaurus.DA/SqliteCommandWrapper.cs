using System;
#if __MonoCS__
using Mono.Data.Sqlite;
#else
using System.Data.SQLite;
#endif

namespace Gorgosaurus.DA
{
    public class SqliteCommandWrapper
    {
        #if __MonoCS__
        private readonly SqliteCommand _command;
        #else
        private readonly SQLiteCommand _command;
        #endif

        #if __MonoCS__
        public SqliteCommandWrapper(string commandText, SqliteConnection connection)
        {
            _command = new SqliteCommand(commandText, connection);
        }
        #else
        public SqliteCommandWrapper(string commandText, SQLiteConnection connection)
        {
            _command = new SQLiteCommand(commandText, connection);
        }
        #endif

        #if __MonoCS__
        public SqliteCommand GetInner()
        #else
        public SQLiteCommand GetInner()
        #endif
        {
            return _command;
        }

    }
}


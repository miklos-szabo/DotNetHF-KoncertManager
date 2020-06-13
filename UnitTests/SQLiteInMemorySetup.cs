using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UnitTests
{
    /**
     * SQLite memóriabeli kapcsolat létrehozása, context kiadása
     */
    class SQLiteInMemorySetup : DBContextSetup, IDisposable
    {
        private readonly DbConnection _connection;

        public SQLiteInMemorySetup(): base(new DbContextOptionsBuilder<ConcertManagerContext>()
            .UseSqlite(CreateInMemoryDatabase())
            .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        /**
         * A connectionnek már nyitva kell lennie, mikor a konstruktornak átadjuk
         */
        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            return connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}

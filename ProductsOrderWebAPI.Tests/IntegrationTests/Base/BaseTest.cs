using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProductsOrderWebAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsOrderWebAPI.Tests.IntegrationTests.Base
{
    public abstract class BaseTest : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AppDbContext _context;

        protected BaseTest()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new AppDbContext(options);

            _context.Database.EnsureCreated();
        }

        protected void ClearDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Close();
        }
    }
}

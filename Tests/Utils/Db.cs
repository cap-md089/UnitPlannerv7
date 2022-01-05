// Db.cs: Database testing utilities
//
// Copyright (C) 2022 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UnitPlanner.Tests.Utils;

public class Db
{
    public static DbContextOptions<T> InMemorySqliteDatabase<T>()
        where T : DbContext
    =>
        new DbContextOptionsBuilder<T>()
            .UseSqlite("Data Source=:memory:;")
            .Options;
}

public abstract class DbBasedTest<T> : IDisposable
    where T : DbContext
{
    private readonly DbConnection? _connection;

    protected DbBasedTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");

        _connection.Open();

        ContextOptions = new DbContextOptionsBuilder<T>()
            .UseSqlite(_connection)
            .Options;
    }

    protected DbBasedTest(DbContextOptions<T> options)
    {
        ContextOptions = options;

        _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
    }

    protected DbContextOptions<T> ContextOptions { get; }

    protected void Clear(T context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public void Dispose() => _connection?.Dispose();
}
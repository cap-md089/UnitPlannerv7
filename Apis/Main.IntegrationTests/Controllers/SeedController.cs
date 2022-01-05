// SeedController: Provides an interface for integration tests to seed the database
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

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UnitPlanner.Apis.Main.Data;
using UnitPlanner.Apis.Main.Models;

namespace UnitPlanner.Apis.Main.IntegrationTests.Controllers;

[ApiController]
[Route("/api/seed/")]
public class SeedController : ControllerBase
{
    private readonly UnitPlannerDbContext _context;

    public SeedController(UnitPlannerDbContext context) =>
        (_context) = (context);

    [HttpGet]
    [Route("current")]
    public async Task<DatabaseState> GetDbState()
    {
        var units = await _context.Units.ToListAsync();
        var events = await _context.Events.ToListAsync();

        return new DatabaseState(units, events);
    }

    [HttpPost]
    [Route("clear")]
    public async Task Clear()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Units;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Events;");
    }

    [HttpPost]
    [Route("write")]
    public async Task Write(DatabaseState state)
    {
        await Clear();

        await _context.Units.AddRangeAsync(state.Units);
        await _context.Events.AddRangeAsync(state.Events);
    }
}

public class DatabaseState
{
    public ICollection<Unit> Units = new List<Unit>();
    public ICollection<CalendarEvent> Events = new List<CalendarEvent>();

    public DatabaseState(ICollection<Unit> units, ICollection<CalendarEvent> events)
    {
        Units = units;
        Events = events;
    }
}
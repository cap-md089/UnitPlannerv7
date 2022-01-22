// EventsService.cs: Provides an interface for managing events
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

using Microsoft.EntityFrameworkCore;
using UnitPlanner.Apis.Main.Data;
using UnitPlanner.Apis.Main.Models;

namespace UnitPlanner.Apis.Main.Services;

public interface IEventsService
{
    Task<CalendarEvent?> GetEvent(Account unit, Guid id);
}

public class EventsService : IEventsService
{
    private readonly UnitPlannerDbContext _context;
    private readonly IAccountsService _unitsService;

    public EventsService(UnitPlannerDbContext context, IAccountsService unitsService) =>
        (_context, _unitsService) = (context, unitsService);

    public async Task<CalendarEvent?> GetEvent(Account unit, Guid id) =>
        (await _context.Entry(unit)
            .Collection(u => u.Calendars)
            .Query()
            .Include(c => c.Events)
            .Where(c => c.Events.Any(e => e.Id == id))
            .FirstOrDefaultAsync())?
            .Events
            .FirstOrDefault(e => e.Id == id);

    public Task<CalendarEvent?> GetEvent(Calendar calendar, Guid id) =>
        _context.Entry(calendar)
            .Collection(c => c.Events)
            .Query()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
}
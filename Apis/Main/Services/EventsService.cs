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
    Task<CalendarEvent?> GetEvent(Unit unit, int id);
}

public class EventsService : IEventsService
{
    private readonly UnitPlannerDbContext _context;
    private readonly IUnitsService _unitsService;

    public EventsService(UnitPlannerDbContext context, IUnitsService unitsService) =>
        (_context, _unitsService) = (context, unitsService);

    public Task<CalendarEvent?> GetEvent(Unit unit, int id) =>
        _context.Entry(unit)
            .Collection(u => u.Events)
            .Query()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
}
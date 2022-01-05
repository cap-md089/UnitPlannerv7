// UnitsService.cs: Provides abstractions for managing Units
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
using Microsoft.AspNetCore.Mvc;

using UnitPlanner.Apis.Main.Models;
using UnitPlanner.Apis.Main.Data;

namespace UnitPlanner.Apis.Main.Services;

public interface IUnitsService
{
    Task<Unit?> GetUnit(string id);

    Task<Unit> CreateNewUnit(string id);

    Task<IEnumerable<Unit>> GetUnits();
}

public class UnitsService : IUnitsService
{
    private readonly UnitPlannerDbContext _context;

    public UnitsService(UnitPlannerDbContext context) =>
        (_context) = (context);

    public async Task<Unit?> GetUnit(string id)
    {
        var unit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);

        return unit;
    }

    public async Task<Unit> CreateNewUnit(string id)
    {
        var unit = new Unit()
        {
            Id = id
        };

        await _context.Units.AddAsync(unit);
        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<IEnumerable<Unit>> GetUnits() =>
        await _context.Units.ToListAsync();
}
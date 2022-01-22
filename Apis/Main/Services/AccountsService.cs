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

using LanguageExt;
using Microsoft.EntityFrameworkCore;

using UnitPlanner.Apis.Main.Models;
using UnitPlanner.Apis.Main.Data;

namespace UnitPlanner.Apis.Main.Services;

public interface IAccountsService
{
    Task<Option<Account>> GetUnit(string id);

    Task<CAPSquadron> CreateNewSquadron(CAPWing wing, CAPGroup group, string id, IEnumerable<Models.NHQ.Organization> organizations);

    Task<CAPGroup> CreateNewGroup(CAPWing wing, string id, IEnumerable<Models.NHQ.Organization> organizations);

    Task<CAPWing> CreateNewWing(string id, IEnumerable<Models.NHQ.Organization> organizations);

    Task<IEnumerable<Account>> GetUnits();
}

public class AccountsService : IAccountsService
{
    private readonly UnitPlannerDbContext _context;

    public AccountsService(UnitPlannerDbContext context) =>
        (_context) = (context);

    public async Task<Option<Account>> GetUnit(string id) =>
        (await _context
            .Accounts
            .Include(a => a.Domains)
            .Include(a => a.Calendars)
            .FirstOrDefaultAsync(u => u.Domains.Any(d => d.Domain == id))) switch
        {
            null => Option<Account>.None,
            Account acc => Option<Account>.Some(acc)
        };

    public async Task<CAPWing> CreateNewWing(string id, IEnumerable<Models.NHQ.Organization> organizations)
    {
        var unit = new CAPWing()
        {
            Id = id,
            Calendars = new List<Calendar>(),
            Domains = new List<AccountDomain>(),
        };

        unit.Domains.Add(new AccountDomain
        {
            Unit = unit,
            Domain = id
        });

        unit.Calendars.Add(new Calendar
        {
            Account = unit,
            Color = "000000",
            Name = "Default Calendar"
        });

        await _context.Accounts.AddAsync(unit);
        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<CAPGroup> CreateNewGroup(CAPWing wing, string id, IEnumerable<Models.NHQ.Organization> organizations)
    {
        var unit = new CAPGroup()
        {
            Id = id,
            Calendars = new List<Calendar>(),
            Domains = new List<AccountDomain>(),
            Wing = wing
        };

        unit.Domains.Add(new AccountDomain
        {
            Unit = unit,
            Domain = id
        });

        unit.Calendars.Add(new Calendar
        {
            Account = unit,
            Color = "000000",
            Name = "Default Calendar"
        });

        await _context.Accounts.AddAsync(unit);
        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<CAPSquadron> CreateNewSquadron(CAPWing wing, CAPGroup group, string id, IEnumerable<Models.NHQ.Organization> organizations)
    {
        var unit = new CAPSquadron()
        {
            Id = id,
            Calendars = new List<Calendar>(),
            Domains = new List<AccountDomain>(),
            Wing = wing,
            Group = group
        };

        unit.Domains.Add(new AccountDomain
        {
            Unit = unit,
            Domain = id
        });

        unit.Calendars.Add(new Calendar
        {
            Account = unit,
            Color = "000000",
            Name = "Default Calendar"
        });

        await _context.Accounts.AddAsync(unit);

        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<IEnumerable<Account>> GetUnits() =>
        await _context.Accounts
            .Include(a => a.Calendars)
            .Include(a => a.Domains)
            .ToListAsync();
}
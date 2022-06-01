// AccountsService.cs: Provides abstractions for managing Units
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
using UnitPlanner.Apis.Main.Services.HostConfiguration;

namespace UnitPlanner.Apis.Main.Services;

public interface IAccountsService
{
    Task<Option<Account>> GetUnit(string id);

    Task<Option<Account>> GetUnitFromUrl(string id);

    Task<CAPSquadron> CreateNewSquadron(CAPWing wing, CAPGroup group, string id, string? baseUrl, IEnumerable<int> organizationsIds);

    Task<CAPGroup> CreateNewGroup(CAPWing wing, string id, string? baseUrl, IEnumerable<int> organizationsIds);

    Task<CAPWing> CreateNewWing(string id, string baseUrl, IEnumerable<int> organizationIds);

    Task<IEnumerable<Account>> GetUnits();

    Task DeleteUnit(Account account);
}

public class AccountsService : IAccountsService
{
    private readonly UnitPlannerDbContext _context;
    private readonly IHostConfigurationService _hostConfiguration;

    public AccountsService(UnitPlannerDbContext context, IHostConfigurationService hostConfiguration) =>
        (_context, _hostConfiguration) = (context, hostConfiguration);

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

    public async Task<Option<Account>> GetUnitFromUrl(string url)
    {
        var foundAccount = await _context
            .Accounts
            .Include(a => a.Domains)
            .Include(a => a.Calendars)
            .FirstOrDefaultAsync(a =>
                (a as CAPWing)!.BaseUrl == url ||
                (a as CAPGroup)!.OverrideBaseUrl == url ||
                (a as CAPSquadron)!.OverrideBaseUrl == url);

        if (foundAccount is not null)
        {
            return Option<Account>.Some(foundAccount);
        }

        var accountIdCheck = url.Split('.')[0];

        return await GetUnit(accountIdCheck);
    }

    public async Task<CAPWing> CreateNewWing(string id, string baseUrl, IEnumerable<int> organizations)
    {
        var unit = new CAPWing()
        {
            Id = id,
            BaseUrl = baseUrl,
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

        await _hostConfiguration.UpdateHosts(id, new List<string> {
            $"{id}.{baseUrl}",
            baseUrl
        });

        await _context.Accounts.AddAsync(unit);
        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<CAPGroup> CreateNewGroup(CAPWing wing, string id, string? baseUrl, IEnumerable<int> organizations)
    {
        var unit = new CAPGroup()
        {
            Id = id,
            OverrideBaseUrl = baseUrl,
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

        var hosts = new List<string> { $"{id}.{wing.BaseUrl}" };
        if (baseUrl is not null)
        {
            hosts.Add(baseUrl);
            hosts.Add($"{id}.{baseUrl}");
        }
        await _hostConfiguration.UpdateHosts(id, hosts);

        await _context.Accounts.AddAsync(unit);
        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<CAPSquadron> CreateNewSquadron(CAPWing wing, CAPGroup group, string id, string? baseUrl, IEnumerable<int> organizations)
    {
        var unit = new CAPSquadron()
        {
            Id = id,
            OverrideBaseUrl = baseUrl,
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

        var hosts = new List<string> { $"{id}.{wing.BaseUrl}" };
        if (group.GetBaseUrl() is string groupBaseUrl)
        {
            hosts.Add($"{id}.{groupBaseUrl}");
        }
        if (baseUrl is not null)
        {
            hosts.Add(baseUrl);
        }
        await _hostConfiguration.UpdateHosts(id, hosts);

        await _context.Accounts.AddAsync(unit);
        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task<IEnumerable<Account>> GetUnits() =>
        await _context.Accounts
            .Include(a => a.Calendars)
            .Include(a => a.Domains)
            .ToListAsync();

    public async Task DeleteUnit(Account account) 
    {
        if (account is CAPActivity activity)
        {
            await _context.Entry(activity)
                .Reference(a => a.Host)
                .LoadAsync();
        }
        else if (account is CAPGroup group)
        {
            await _context.Entry(group)
                .Reference(g => g.Wing)
                .LoadAsync();
        }
        else if (account is CAPSquadron squadron)
        {
            await _context.Entry(squadron)
                .Reference(g => g.Group)
                .LoadAsync();
            await _context.Entry(squadron)
                .Reference(g => g.Wing)
                .LoadAsync();
        }

        await _hostConfiguration.RemoveHost(account.Id);

        _context.Accounts.Remove(account);

        await _context.SaveChangesAsync();
    }
}

public static class AccountExtensions
{
    public static string GetBaseUrl(this Account account) => account switch
    {
        CAPActivity activity => activity.OverrideBaseUrl ?? activity.Host.GetBaseUrl(),
        CAPWing wing => wing.BaseUrl,
        CAPGroup group => group.OverrideBaseUrl ?? group.Wing.BaseUrl,
        CAPSquadron squadron => squadron.OverrideBaseUrl ?? squadron.Group.OverrideBaseUrl ?? squadron.Wing.BaseUrl,
        _ => throw new Exception($"Unknown account type: {account.Type.ToString()}")
    };
}
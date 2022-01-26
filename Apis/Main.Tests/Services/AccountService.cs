// UnitService.cs: API test and example code for the units service
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using UnitPlanner.Apis.Main.Data;
using UnitPlanner.Apis.Main.Models;
using UnitPlanner.Apis.Main.Services;
using UnitPlanner.Apis.Main.Services.HostConfiguration;
using UnitPlanner.Tests.Utils;

namespace UnitPlanner.Apis.Main.Tests;

public class AccountService : DbBasedTest<UnitPlannerDbContext>
{
    public AccountService()
        : base()
    {
    }

    [Fact]
    public void Can_create_new_wing()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);
        Clear(context);

        Assert.Equal(0, context.Accounts.Count());

        var hostService = new TestHostConfigurationService();
        var service = new AccountsService(context, hostService);
        var result = service.CreateNewWing("md001", "localevmplus.org", new List<Models.NHQ.Organization>()).Result;

        Assert.Equal("md001", result.Id);
        Assert.Equal(1, context.Accounts.Count());
    }

    [Fact]
    public void Can_create_new_group()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);
        Clear(context);

        var hostService = new TestHostConfigurationService();
        var service = new AccountsService(context, hostService);
        
        var wing = service.CreateNewWing("md001", "localevmplus.org", new List<Models.NHQ.Organization>()).Result;

        Assert.Equal(1, context.Accounts.Count());

        var result = service.CreateNewGroup(wing, "md043", null, new List<Models.NHQ.Organization>()).Result;

        Assert.Equal("md043", result.Id);
        Assert.Equal(2, context.Accounts.Count());
    }

    [Fact]
    public void Can_create_new_squadron()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);
        Clear(context);

        var hostService = new TestHostConfigurationService();
        var service = new AccountsService(context, hostService);

        var wing = service.CreateNewWing("md001", "localevmplus.org", new List<Models.NHQ.Organization>()).Result;
        var group = service.CreateNewGroup(wing, "md043", null, new List<Models.NHQ.Organization>()).Result;

        Assert.Equal(2, context.Accounts.Count());

        var result = service.CreateNewSquadron(wing, group, "md089", null, new List<Models.NHQ.Organization>()).Result;

        Assert.Equal("md089", result.Id);
        Assert.Equal(3, context.Accounts.Count());
    }

    [Fact]
    public void Can_enumerate_units()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);
        Clear(context);

        var hostService = new TestHostConfigurationService();
        var service = new AccountsService(context, hostService);

        var wing = service.CreateNewWing("md001", "localevmplus.org", new List<Models.NHQ.Organization>()).Result;
        var group = service.CreateNewGroup(wing, "md043", null, new List<Models.NHQ.Organization>()).Result;
        var squadron = service.CreateNewSquadron(wing, group, "md089", null, new List<Models.NHQ.Organization>()).Result;

        context.SaveChanges();

        var result = service.GetUnits().Result;

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Can_get_unit()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);
        Clear(context);

        var hostService = new TestHostConfigurationService();
        var service = new AccountsService(context, hostService);

        _ = service.CreateNewWing("md001", "localevmplus.org", new List<Models.NHQ.Organization>()).Result;

        Account? result = service.GetUnit("md001").Result.Case as Account;

        Assert.Equal("md001", result?.Id);
    }
}

internal class TestHostConfigurationService : IHostConfigurationService
{
    public Task AddNewHost(string id, string baseUrl, string hostName) => Task.CompletedTask;
    public Task RemoveHost(string id, string baseUrl, string hostName) => Task.CompletedTask;
}
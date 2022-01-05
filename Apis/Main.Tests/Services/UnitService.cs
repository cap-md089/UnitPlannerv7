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
using Xunit;

using UnitPlanner.Apis.Main.Data;
using UnitPlanner.Apis.Main.Models;
using UnitPlanner.Apis.Main.Services;
using UnitPlanner.Tests.Utils;

namespace UnitPlanner.Apis.Main.Tests;

public class UnitService : DbBasedTest<UnitPlannerDbContext>
{
    public UnitService()
        : base ()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [Fact]
    public void Can_create_new_unit()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);

        Clear(context);

        Assert.Equal(0, context.Units.Count());

        var service = new UnitsService(context);

        var result = service.CreateNewUnit("md089").Result;

        Assert.Equal("md089", result.Id);
        Assert.Equal(1, context.Units.Count());
    }

    [Fact]
    public void Can_enumerate_units()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);

        Clear(context);

        context.Units.Add(new Models.Unit() { Id = "md001" });
        context.Units.Add(new Models.Unit() { Id = "md089" });
        context.Units.Add(new Models.Unit() { Id = "md890" });
        context.SaveChanges();

        var service = new UnitsService(context);

        var result = service.GetUnits().Result;

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void Can_get_unit()
    {
        using var context = new UnitPlannerDbContext(ContextOptions);

        Clear(context);

        context.Units.Add(new Models.Unit() { Id = "md001" });
        context.SaveChanges();

        var service = new UnitsService(context);

        var result = service.GetUnit("md001").Result;

        Assert.Equal("md001", result?.Id);
    }
}
// DbContextFactory.cs: Helps configure the database for the command line
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
using Microsoft.EntityFrameworkCore.Design;

namespace UnitPlanner.Apis.Main.Data;

class UnitPlannerDbContextFactory : IDesignTimeDbContextFactory<UnitPlannerDbContext>
{
    public UnitPlannerDbContext CreateDbContext(string[] args)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
        var optionsBuilder = new DbContextOptionsBuilder<UnitPlannerDbContext>();
        optionsBuilder.UseMySql("server=db;uid=root;password=toor;database=UnitPlannerv7", serverVersion);

        return new UnitPlannerDbContext(optionsBuilder.Options);
    }
}
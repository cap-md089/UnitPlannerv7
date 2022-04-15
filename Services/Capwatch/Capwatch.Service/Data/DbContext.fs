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

module UnitPlanner.Services.Capwatch.Service.Data

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

open UnitPlanner.Services.Capwatch.Service.Models

type CapwatchDbContext(options : DbContextOptions<CapwatchDbContext>) =
    inherit DbContext(options)

    override this.OnModelCreating(modelBuilder : ModelBuilder) =
        modelBuilder.Entity<Member>();

        modelBuilder.Entity<CadetAchv>()
            .HasKey(fun ca -> (ca.CAPID, ca.CadetAchvID) :> obj)

        modelBuilder.Entity<CadetAchvAprs>()
            .HasKey(fun ca -> (ca.CAPID, ca.CadetAchvID) :> obj)

        modelBuilder.Entity<CadetActivities>()
            .HasKey(fun ca -> (ca.CAPID, ca.Completed) :> obj)

        modelBuilder.Entity<CadetDutyPosition>()
            .HasKey(fun dp -> (dp.CAPID, dp.Duty, dp.ORGID, dp.Asst) :> obj)

        modelBuilder.Entity<DutyPosition>()
            .HasKey(fun dp -> (dp.CAPID, dp.Duty, dp.ORGID, dp.Asst) :> obj)

        modelBuilder.Entity<MbrContact>()
            .HasKey(fun mc -> (mc.CAPID, mc.Type, mc.Priority) :> obj)

        modelBuilder.Entity<OFlight>()
            .HasKey(fun f -> (f.CAPID, f.FltDate) :> obj)
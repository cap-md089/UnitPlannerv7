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
open EntityFrameworkCore.FSharp.Extensions

open UnitPlanner.Services.Capwatch.Service.Models

type CapwatchDbContext(options : DbContextOptions<CapwatchDbContext>) as this =
    inherit DbContext(options)

    let CadetAchvs = this.Set<CadetAchvM>()
    let CadetAchvAprs = this.Set<CadetAchvAprsM>()
    let CadetActivities = this.Set<CadetActivitiesM>()
    let CadetDutyPositions = this.Set<CadetDutyPositionM>()
    let CadetHFZInformation = this.Set<CadetHFZInformationM>()
    let CadetAchvEnum = this.Set<CdtAchvEnum>()
    let DutyPositions = this.Set<DutyPositionM>()
    let MbrContact = this.Set<MbrContactM>()
    let OFlights = this.Set<OFlightM>()
    let Organizations = this.Set<OrganizationM>()
    let Members = this.Set<Member>()

    override this.OnModelCreating(modelBuilder : ModelBuilder) =
        modelBuilder.Entity<Member>()
            |> ignore

        modelBuilder.Entity<CadetAchvM>()
            .HasKey(fun ca -> (ca.CAPID, ca.CadetAchvID) :> obj)
            |> ignore

        modelBuilder.Entity<CadetAchvAprsM>()
            .HasKey(fun ca -> (ca.CAPID, ca.CadetAchvID) :> obj)
            |> ignore

        modelBuilder.Entity<CadetActivitiesM>()
            .HasKey(fun ca -> (ca.CAPID, ca.Completed) :> obj)
            |> ignore

        modelBuilder.Entity<CadetDutyPositionM>()
            .HasKey(fun dp -> (dp.CAPID, dp.Duty, dp.ORGID, dp.Asst) :> obj)
            |> ignore

        modelBuilder.Entity<DutyPositionM>()
            .HasKey(fun dp -> (dp.CAPID, dp.Duty, dp.ORGID, dp.Asst) :> obj)
            |> ignore

        modelBuilder.Entity<MbrContactM>()
            .HasKey(fun mc -> (mc.CAPID, mc.Type, mc.Priority) :> obj)
            |> ignore

        modelBuilder.Entity<OFlightM>()
            .HasKey(fun f -> (f.CAPID, f.FltDate) :> obj)
            |> ignore

    override this.OnConfiguring(options : DbContextOptionsBuilder) : unit =
        options.UseFSharpTypes() |> ignore
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
namespace UnitPlanner.Services.Capwatch.Service.Models

open System
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open Microsoft.EntityFrameworkCore

[<TableAttribute("NHQ_CdtAchvEnum")>]
type CdtAchvEnum(CadetAchvID : int, AchvName : string, CurAwdNo : int, UsrID : string, DateMod : DateTime,
                FirstUsr : string, DateCreated : DateTime, Rank : string) =

    [<Key>]
    member this.CadetAchvId = CadetAchvID

    member this.AchvName = AchvName

    member this.CurAwdNo = CurAwdNo

    member this.UsrID = UsrID

    member this.DateMod = DateMod

    member this.FirstUsr = FirstUsr

    member this.DateCreated = DateCreated

    member this.Rank = Rank

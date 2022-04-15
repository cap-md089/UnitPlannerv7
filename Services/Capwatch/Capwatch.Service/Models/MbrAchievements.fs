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

[<Table("NHQ_DutyPosition")>]
type MbrAchievements<'a> (CAPID : int, Member : 'a, AchvID : int, Status : string, OriginallyAccomplished : DateTime,
                            Completed : DateTime, Expiration : DateTime, AuthByCAPID : int, AuthReason : string, AuthDate : DateTime,
                            Source : string, RecID : int, FirstUsr : string, DateCreated : DateTime, UsrID : string,
                            DateMod : DateTime, ORGID : int) =
    member this.CAPID = CAPID 
    member this.AchvID = AchvID
    [<StringLength(25)>]
    member this.Status = Status
    member this.OriginallyAccomplished = OriginallyAccomplished
    member this.Completed = Completed
    member this.Expiration = Expiration
    member this.AuthByCAPID = AuthByCAPID
    [<StringLength(25)>]
    member this.AuthReason = AuthReason
    member this.AuthDate = AuthDate
    [<StringLength(25)>]
    member this.Source = Source
    member this.RecID = RecID
    [<StringLength(25)>]
    member this.UsrID = UsrID
    member this.DateMod = DateMod
    member this.ORGID = ORGID


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
open System.ComponentModel.DataAnnotations.Schema

[<Table("NHQ_OFlight")>]
type OFlight<'a>(CAPID : int, Member : 'a, Wing : string, Unit : string,
                    Amount : double, Syllabus : int, Type : string,
                    FltDate : DateTime, TransDate : DateTime,
                    FltRlsNum : string, AcftTailNum : string,
                    FltTime : double, LstUser : string,
                    LstDateMod : DateTime, Comments : string) =

    member this.CAPID = CAPID
    [<ForeignKey("CAPID")>]
    member this.Member = Member

    member this.Wing = Wing

    member this.Unit = Unit

    member this.Amount = Amount

    member this.Syllabus = Syllabus

    member this.Type = Type

    member this.FltDate = FltDate

    member this.TransDate = TransDate

    member this.FltRlsNum = FltRlsNum

    member this.AcftTailNum = AcftTailNum

    member this.FltTime = FltTime

    member this.LstUser = LstUser

    member this.LstDateMod = LstDateMod

    member this.Comments = Comments
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
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema

[<TableAttribute("NHQ_Oragnization")>]
type Organization<'a>(ORGID : int, Region : string, Wing : string,
                        Unit : string, NextLevel : int option,
                        Parent : Organization<'a> option, Name : string,
                        Type : string, DateChartered : DateTime,
                        Status : string, Scope : string,
                        UsrID : string, DateMod : DateTime,
                        FirstUsr : string, DateCreated : DateTime,
                        DateReceived : DateTime, OrgNotes : string,
                        Members : ICollection<'a>,
                        SubordinateOrganizations : ICollection<Organization<'a>>) =
    [<Key>]
    member this.ORGID = ORGID

    member this.Region = Region

    member this.Wing = Wing

    member this.Unit = Unit

    member this.NextLevel = NextLevel
    member this.Parent = Parent

    member this.Name = Name
    
    member this.Type = Type
    
    member this.DateChartered = DateChartered

    member this.Status = Status

    member this.Scope = Scope

    member this.UsrID = UsrID

    member this.DateMod = DateMod

    member this.FirstUsr = FirstUsr

    member this.DateCreated = DateCreated

    member this.DateReceived = DateReceived

    member this.OrgNotes = OrgNotes

    member this.Members = Members

    member this.SubordinateOrganizations = SubordinateOrganizations